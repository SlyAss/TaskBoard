using System;
using System.Collections.Concurrent;
using TaskBoard.Client.Clients;

namespace TaskBoard.Client {
	public class HttpClientProvider {
		private readonly ConcurrentDictionary<string, BaseHttpClient> hashedClients;
		private readonly HttpClientParameters httpClientParameters;
		public string ServerAddress => httpClientParameters.ServerAddress;
		public int TimeoutMs => httpClientParameters.TimeoutMs;
		public string Login => httpClientParameters.Login;
		public string Password => httpClientParameters.Password;

		public HttpClientProvider(string serverAddress, int timeoutMs, string login, string password) {
			hashedClients = new ConcurrentDictionary<string, BaseHttpClient>();
			httpClientParameters = new HttpClientParameters {
				ServerAddress = serverAddress,
				TimeoutMs = timeoutMs,
				Login = login,
				Password = password
			};
		}

		public ParameretsClient GetParameretsClient() {
			return GetClient("ParameretsClient", () => new ParameretsClient(httpClientParameters));
		}

		private TClient GetClient<TClient>(string clientName, Func<TClient> createClient) where TClient : BaseHttpClient {
			return (TClient)hashedClients.GetOrAdd(clientName, name => createClient());
		}
	}
}