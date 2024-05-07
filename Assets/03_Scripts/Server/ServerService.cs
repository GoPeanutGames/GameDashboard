using System;
using System.Text;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace PeanutDashboard.Server
{
	public static class ServerService
	{
		private static string Encrypt(string jsonForm)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(Encrypt)}");
			EncryptedData jsonEncryptedForm = new()
			{
				data = RSAUtility.Encrypt(jsonForm)
			};
			return JsonUtility.ToJson(jsonEncryptedForm);
		}

		private static string Decrypt(string formEncryptedData)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(Decrypt)}");
			EncryptedData jsonEncryptedData = JsonUtility.FromJson<EncryptedData>(formEncryptedData);
			return RSAUtility.Decrypt(jsonEncryptedData.data);
		}

		private static UnityWebRequest SetupPostWebRequest(string api, string formData)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SetupPostWebRequest)}");
			string encryptedFormData = EnvironmentManager.Instance.IsRSAActive() ? Encrypt(formData) : formData;
			string serverUrl = EnvironmentManager.Instance.GetServerUrl();
			UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(serverUrl + api, encryptedFormData);
			UploadHandler customUploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(encryptedFormData));
			customUploadHandler.contentType = "application/json";
			webRequest.uploadHandler = customUploadHandler;
			webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
			webRequest.SetRequestHeader("Content-Type", "application/json");
			webRequest.SetRequestHeader("Accept", "*/*");
			return webRequest;
		}

		private static UnityWebRequest SetupGetWebRequest(string api)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SetupGetWebRequest)}");
			string serverUrl = EnvironmentManager.Instance.GetServerUrl();
			UnityWebRequest webRequest = UnityWebRequest.Get(serverUrl + api);

			webRequest.SetRequestHeader("Content-Type", "application/json");
			webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
			webRequest.SetRequestHeader("Accept", "*/*");
			return webRequest;
		}

		private static void SendWebRequest(UnityWebRequest webRequest, Action<string> onComplete, Action<string> onFail = null)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SendWebRequest)}");
			AsyncOperation operation = webRequest.SendWebRequest();
			operation.completed += (result) =>
			{
				string data = webRequest.downloadHandler.text;
				string decryptedData = EnvironmentManager.Instance.IsRSAActive() ? Decrypt(data) : data;
				if (webRequest.result == UnityWebRequest.Result.Success){
					onComplete?.Invoke(decryptedData);
				}
				else{
					onFail?.Invoke(decryptedData);
				}

				webRequest.Dispose();
			};
		}
		
		private static void SendWebRequest<T,U>(UnityWebRequest webRequest, Action<T> onComplete, Action<U> onFail = null)
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SendWebRequest)}");
			AsyncOperation operation = webRequest.SendWebRequest();
			operation.completed += (result) =>
			{
				string data = webRequest.downloadHandler.text;
				string decryptedData = EnvironmentManager.Instance.IsRSAActive() ? Decrypt(data) : data;
				if (webRequest.result == UnityWebRequest.Result.Success){
					LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SendWebRequest)} - complete with data: {decryptedData}");
					T completeData = JsonUtility.FromJson<T>(decryptedData);
					onComplete?.Invoke(completeData);
				}
				else{
					LoggerService.LogWarning($"{nameof(ServerService)}::{nameof(SendWebRequest)} - fail with data: {decryptedData}");
					U failData = JsonUtility.FromJson<U>(decryptedData);
					onFail?.Invoke(failData);
				}

				webRequest.Dispose();
			};
		}

		public static void GetDataFromServer<T, U, V>(T api, Action<U> onComplete, string address = "", Action<V> onFail = null) where T : struct, IConvertible
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(GetDataFromServer)} - {api}, with {address}");
			UnityWebRequest webRequest = SetupGetWebRequest(ApiReference.GetApi(api) + address);
			SendWebRequest(webRequest, onComplete, onFail);
		}

		public static void GetDataFromServer<T>(T api, Action<string> onComplete, string address = "", Action<string> onFail = null) where T : struct, IConvertible
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(GetDataFromServer)} - {api}, with {address}");
			UnityWebRequest webRequest = SetupGetWebRequest(ApiReference.GetApi(api) + address);
			SendWebRequest(webRequest, onComplete, onFail);
		}
		
		public static void PostDataToServer<T>(T api, string formData, Action<string> onComplete, Action<string> onFail = null) where T : struct, IConvertible
		{
			LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(PostDataToServer)} - {api}, with {formData}");
			UnityWebRequest webRequest = SetupPostWebRequest(ApiReference.GetApi(api), formData);
			SendWebRequest(webRequest, onComplete, onFail);
		}
	}
}