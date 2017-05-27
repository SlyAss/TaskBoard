using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseUserReader : BaseHttpClient, IDatabaseUserReader {
		public DatabaseUserReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public User[] GetAll() {
			return SendRequest<User[]>("GetAllUsers", GetDefaultParameters());
		}

		public User[] GetWithUsingFilters(string login) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.UserLogin, login);

			return SendRequest<User[]>("GetAllUsers", parameters);
		}
	}
}