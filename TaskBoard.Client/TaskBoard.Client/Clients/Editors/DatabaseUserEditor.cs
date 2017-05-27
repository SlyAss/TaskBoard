using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Editors {
	public class DatabaseUserEditor : BaseHttpClient, IDatabaseUserEditor {
		public DatabaseUserEditor(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public void Add(User table) {
			SendRequest("AddUser", GetDefaultParameters(), table.ToJson());
		}

		public void Edit(Guid oldTableId, User newTable) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.UserId] = oldTableId.ToString();

			SendRequest("EditUser", parameters, newTable.ToJson());
		}

		public void Delete(Guid tableId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.UserId] = tableId.ToString();

			SendRequest("DeleteUser", parameters);
		}
	}
}