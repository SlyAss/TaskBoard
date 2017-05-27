using System;
using System.Collections.Concurrent;
using TaskBoard.Client.Clients;
using TaskBoard.Client.Clients.Editors;
using TaskBoard.Client.Clients.Readers;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Database.Readers;

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

		public IDatabaseBoardEditor GetDatabaseBoardEditor() {
			return GetClient("DatabaseBoardEditor", () => new DatabaseBoardEditor(httpClientParameters));
		}
		public IDatabaseColumnEditor GetDatabaseColumnEditor() {
			return GetClient("DatabaseColumnEditor", () => new DatabaseColumnEditor(httpClientParameters));
		}
		public IDatabaseCommentEditor GetDatabaseCommentEditor() {
			return GetClient("DatabaseCommentEditor", () => new DatabaseCommentEditor(httpClientParameters));
		}
		public IDatabaseTaskEditor GetDatabaseTaskEditor() {
			return GetClient("DatabaseTaskEditor", () => new DatabaseTaskEditor(httpClientParameters));
		}
		public IDatabaseUserEditor GetDatabaseUserEditor() {
			return GetClient("DatabaseUserEditor", () => new DatabaseUserEditor(httpClientParameters));
		}

		public IDatabaseBoardReader GetDatabaseBoardReader() {
			return GetClient("DatabaseBoardReader", () => new DatabaseBoardReader(httpClientParameters));
		}
		public IDatabaseColumnReader GetDatabaseColumnReader() {
			return GetClient("DatabaseColumnReader", () => new DatabaseColumnReader(httpClientParameters));
		}
		public IDatabaseCommentReader GetDatabaseCommentReader() {
			return GetClient("DatabaseCommentReader", () => new DatabaseCommentReader(httpClientParameters));
		}
		public IDatabaseTaskReader GetDatabaseTaskReader() {
			return GetClient("DatabaseTaskReader", () => new DatabaseTaskReader(httpClientParameters));
		}
		public IDatabaseUserReader GetDatabaseUserReader() {
			return GetClient("DatabaseUserReader", () => new DatabaseUserReader(httpClientParameters));
		}

		private TClient GetClient<TClient>(string clientName, Func<TClient> createClient) where TClient : BaseHttpClient {
			return (TClient)hashedClients.GetOrAdd(clientName, name => createClient());
		}
	}
}