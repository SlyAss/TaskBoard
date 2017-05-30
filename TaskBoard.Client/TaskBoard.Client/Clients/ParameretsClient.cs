using System;
using System.Collections.Generic;
using TaskBoard.Common.Http;

namespace TaskBoard.Client.Clients {
	public class ParameretsClient : BaseHttpClient {
		private readonly HttpClientParameters httpClientParameters;

		public ParameretsClient(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
			this.httpClientParameters = httpClientParameters;
		}

		public void SetServerAddress(string serverAddress, int timeoutMs) {
			if (string.IsNullOrEmpty(serverAddress))
				return;

			serverAddress = new UriBuilder(serverAddress).Uri.ToString();

			new ParameretsClient(new HttpClientParameters { ServerAddress = serverAddress, TimeoutMs = timeoutMs }).SendRequest("Ping");
			httpClientParameters.ServerAddress = serverAddress;
			httpClientParameters.TimeoutMs = timeoutMs;
		}

		public void SignIn(string login, string password) {
			if (httpClientParameters.IsAuthorize)
				throw new ArgumentException("Пользователь уже авторизован");

			var parameters = new Dictionary<string, string> {
				[HttpParameters.Login] = login,
				[HttpParameters.Password] = password
			};

			SendRequest("CheckUserIsExist", parameters);
			httpClientParameters.Login = login;
			httpClientParameters.Password = password;
			httpClientParameters.IsAuthorize = true;
		}

		public void SingOut() {
			if (!httpClientParameters.IsAuthorize)
				throw new ArgumentException("Пользователь не авторизован");

			httpClientParameters.Login = null;
			httpClientParameters.Password = null;
			httpClientParameters.IsAuthorize = false;
		}
	}
}