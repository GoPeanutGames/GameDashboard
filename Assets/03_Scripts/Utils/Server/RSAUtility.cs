using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using PeanutDashboard.Shared.Environment;

namespace PeanutDashboard.Utils
{
	public class RSAUtility
	{
		public static string[] testKey =
		{
			"-----BEGIN RSA PUBLIC KEY-----",
			"MIIBCgKCAQEA15Pmyp5Z4u/kKUvRdeiajT2IQIusKAWqn131ZWkzQQbQzHs8Sls4",
			"7UkiLglQAGUDnkhlKuMelKnGAVwyT/rA7ikcz4d1YrkWaB2940kzcN4q4sZnvNjL",
			"q4Pno/9wDGCURj4jMRChP5aswCoyRLdhdZykyOFDXzpMTD8i7ltxO4LuxBqSrqvL",
			"cCOCs4Q59H8UfRE3cmBxxzFjSVXr9Weor5lxMJ4yjDCPNQ0HEOnh8y3wUbv6aJPe",
			"Wk8N65jBy9XVT6sTCUPKq0E8OW1utjPweMjt1fFKTt24yIXT5eMBslfd4OGamYAP",
			"ubdkkuF348HsabuRZs3FdC93cJErpbFETwIDAQAB",
			"-----END RSA PUBLIC KEY-----",
		};

		private static List<string> SplitDataIntoList(string data)
		{
			List<string> split = new();
			while (data.Length > 200){
				split.Add(data.Substring(0, 200));
				data = data.Remove(0, 200);
			}

			split.Add(data);
			return split;
		}

		public static string Decrypt(List<string> encryptedData, string publicKey = "")
		{
			publicKey = string.IsNullOrEmpty(publicKey) ? EnvironmentManager.Instance.GetCurrentPublicKey() : publicKey;

			List<string> decryptedData = new();
			foreach (string part in encryptedData){
				byte[] bytesToDecrypt = Convert.FromBase64String(part);
				Pkcs1Encoding engine = new Pkcs1Encoding(new RsaEngine());
				using (StringReader txtReader = new StringReader(publicKey)){
					AsymmetricKeyParameter keyParam = (AsymmetricKeyParameter)new PemReader(txtReader).ReadObject();
					engine.Init(false, keyParam);
				}

				string decrypted =
					Encoding.UTF8.GetString(engine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
				decryptedData.Add(decrypted);
			}

			return string.Join("", decryptedData);
		}

		public static List<string> Encrypt(string data, string publicKey = "")
		{
			publicKey = string.IsNullOrEmpty(publicKey) ? EnvironmentManager.Instance.GetCurrentPublicKey() : publicKey;

			List<string> encryptedData = new();
			List<string> spltData = SplitDataIntoList(data);
			foreach (string part in spltData){
				byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(part);
				Pkcs1Encoding engine = new Pkcs1Encoding(new RsaEngine());
				using (StringReader txtReader = new StringReader(publicKey)){
					AsymmetricKeyParameter keyParam = (AsymmetricKeyParameter)new PemReader(txtReader).ReadObject();
					engine.Init(true, keyParam);
				}

				string encrypted =
					Convert.ToBase64String(engine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
				encryptedData.Add(encrypted);
			}

			return encryptedData;
		}

	}
}