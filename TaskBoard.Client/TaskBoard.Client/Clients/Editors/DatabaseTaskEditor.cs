using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Editors {
	public class DatabaseTaskEditor : BaseHttpClient, IDatabaseTaskEditor {
		public DatabaseTaskEditor(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public void Add(Task table) {
			SendRequest("AddTask", GetDefaultParameters(), table.ToJson());
		}

		public void Edit(Guid oldTableId, Task newTable) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskId] = oldTableId.ToString();

			SendRequest("EditTask", parameters, newTable.ToJson());
		}

		public void Delete(Guid tableId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskId] = tableId.ToString();

			SendRequest("DeleteTask", parameters);
		}
	}
}