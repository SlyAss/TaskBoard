using System.Collections.Generic;
using System.Linq;
using System.Net;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;

namespace TaskBoard.Client.Clients {
	// ReSharper disable UnusedMember.Global

	public abstract class BaseHttpClient {
		private readonly HttpClientParameters httpClientParameters;

		protected BaseHttpClient(HttpClientParameters httpClientParameters) {
			this.httpClientParameters = httpClientParameters;
		}

		protected Dictionary<string, string> GetDefaultParameters() {
			return new Dictionary<string, string> {
				{ HttpParameters.Login, httpClientParameters.Login },
				{ HttpParameters.Password, httpClientParameters.Password }
			};
		}
		protected static void AddParameterIfNotNullOrEmpty(Dictionary<string, string> parameters, string key, string value) {
			if (!string.IsNullOrEmpty(value))
				parameters[key] = value;
		}

		protected void SendRequest(string methodName, byte[] requestBody = null) {
			SendRequest(methodName, new Dictionary<string, string>(), requestBody);
		}
		protected void SendRequest(string methodName, Dictionary<string, string> parameters, byte[] requestBody = null) {
			var webRequest = CreateWebRequest(methodName, parameters, requestBody);
			SendRequest(webRequest);
		}
		protected TKey SendRequest<TKey>(string methodName, byte[] requestBody = null) {
			return SendRequest<TKey>(methodName, new Dictionary<string, string>(), requestBody);
		}
		protected TKey SendRequest<TKey>(string methodName, Dictionary<string, string> parameters, byte[] requestBody = null) {
			var webRequest = CreateWebRequest(methodName, parameters, requestBody);
			return GetAnswer<TKey>(SendRequest(webRequest));
		}

		private HttpWebRequest CreateWebRequest(string methodName, Dictionary<string, string> parameters, byte[] requestBody) {
			var webRequest = (HttpWebRequest)WebRequest.Create(httpClientParameters.ServerAddress + CreateRequestUri(methodName, parameters));
			webRequest.Timeout = httpClientParameters.TimeoutMs;

			return requestBody == null
				? CreateGetRequest(webRequest)
				: CreatePostRequest(webRequest, requestBody);
		}
		private string CreateRequestUri(string methodName, Dictionary<string, string> parameters) {
			return parameters.Any() ? $"{methodName}?{string.Join("&", parameters.Select(parameter => $"{parameter.Key}={parameter.Value}"))}" : methodName;
		}
		private HttpWebRequest CreateGetRequest(HttpWebRequest webRequest) {
			webRequest.Method = "GET";
			return webRequest;
		}
		private HttpWebRequest CreatePostRequest(HttpWebRequest webRequest, byte[] requestBody) {
			webRequest.Method = "POST";
			webRequest.ContentLength = requestBody.Length;
			webRequest.GetRequestStream().WriteAndDispose(requestBody);

			return webRequest;
		}

		private HttpWebResponse SendRequest(HttpWebRequest webRequest) {
			try {
				return (HttpWebResponse)webRequest.GetResponse();
			}
			catch (WebException exception) {
				throw new WebException(string.Join("\n",
					new[] {
						exception.Message,
						exception.Response?.GetResponseStream().ReadAndDispose().FromJson<string>()
					}.Where(message => !string.IsNullOrEmpty(message))));
			}
		}
		private TKey GetAnswer<TKey>(HttpWebResponse webResponse) {
			var outputBytes = webResponse.GetResponseStream().ReadAndDispose();
			return outputBytes.Any() ? outputBytes.FromJson<TKey>() : default(TKey);
		}
	}
}