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
    public class ServerService : Singleton<ServerService>
    {
        private string Encrypt(string jsonForm)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(Encrypt)}");
            EncryptedData jsonEncryptedForm = new()
            {
                data = RSAUtility.Encrypt(jsonForm)
            };
            return JsonUtility.ToJson(jsonEncryptedForm);
        }

        private string Decrypt(string formEncryptedData)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(Decrypt)}");
            EncryptedData jsonEncryptedData = JsonUtility.FromJson<EncryptedData>(formEncryptedData);
            return RSAUtility.Decrypt(jsonEncryptedData.data);
        }

        private UnityWebRequest SetupPostWebRequest(string api, string formData)
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

        private UnityWebRequest SetupGetWebRequest(string api)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SetupGetWebRequest)}");
            string serverUrl = EnvironmentManager.Instance.GetServerUrl();
            UnityWebRequest webRequest = UnityWebRequest.Get(serverUrl + api);

            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            webRequest.SetRequestHeader("Accept", "*/*");
            return webRequest;
        }

        private void SendWebRequest(UnityWebRequest webRequest, Action<string> onComplete, Action<string> onFail = null)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(SendWebRequest)}");
            AsyncOperation operation = webRequest.SendWebRequest();
            operation.completed += (result) =>
            {
                string data = webRequest.downloadHandler.text;
                string decryptedData = EnvironmentManager.Instance.IsRSAActive() ? Decrypt(data) : data;
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    onComplete?.Invoke(decryptedData);
                }
                else
                {
                    onFail?.Invoke(decryptedData);
                }

                webRequest.Dispose();
            };
        }

        public void GetSchemaDataFromServer(AuthenticationApi api, Action<string> onComplete, string address)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(GetSchemaDataFromServer)} - {api}, with {address}");
            UnityWebRequest webRequest = SetupGetWebRequest(ApiReference.AuthApiMap[api] + address);
            SendWebRequest(webRequest, onComplete);
        }

        public void CheckWeb3Login(AuthenticationApi api, string formData, Action<string> onComplete)
        {
            LoggerService.LogInfo($"{nameof(ServerService)}::{nameof(CheckWeb3Login)} - {api}, with {formData}");
            UnityWebRequest webRequest = SetupPostWebRequest(ApiReference.AuthApiMap[api], formData);
            SendWebRequest(webRequest, onComplete);
        }
        
        public void GetPlayerDataFromServer(PlayerApi api, Action<string> onComplete, string address = "",
            Action<string> onFail = null)
        {
            UnityWebRequest webRequest = SetupGetWebRequest(ApiReference.PlayerApiMap[api] + address);
            SendWebRequest(webRequest, onComplete, onFail);
        }
    }
}