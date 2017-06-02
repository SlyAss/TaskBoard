using System;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseUserReader : BaseHttpClient, IDatabaseUserReader {
		public DatabaseUserReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public User GetById(Guid id) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.UserId] = id.ToString();

			return SendRequest<User>("GetUserById", parameters);
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