using System;
using System.Collections.Generic;
using TaskBoard.Common.Http;

namespace TaskBoard.Client.Clients {
	public class ParameretsClient : BaseHttpClient {
		private readonly HttpClientParameters httpClientParameters;

		public ParameretsClient(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
			this.httpClientParameters = httpClientParameters;
		}

		public void SetServerAddressAndTimeoutMs(string serverAddress, int timeoutMs) {
			serverAddress = new UriBuilder(serverAddress).Uri.ToString();

			new ParameretsClient(new HttpClientParameters { ServerAddress = serverAddress, TimeoutMs = timeoutMs }).SendRequest("Ping");
			httpClientParameters.ServerAddress = serverAddress;
			httpClientParameters.TimeoutMs = timeoutMs;
		}
		public void SetLoginAndPassword(string login, string password) {
			var parameters = new Dictionary<string, string> {
				[HttpParameters.Login] = login,
				[HttpParameters.Password] = password
			};

			SendRequest("CheckUserIsExist", parameters);
			httpClientParameters.Login = login;
			httpClientParameters.Password = password;
		}
	}
}