using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Editors {
	public class DatabaseBoardEditor : BaseHttpClient, IDatabaseBoardEditor {
		public DatabaseBoardEditor(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public void Add(Board table) {
			SendRequest("AddBoard", GetDefaultParameters(), table.ToJson());
		}

		public void Edit(Guid oldTableId, Board newTable) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.BoardId] = oldTableId.ToString();

			SendRequest("EditBoard", parameters, newTable.ToJson());
		}

		public void Delete(Guid tableId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.BoardId] = tableId.ToString();

			SendRequest("DeleteBoard", parameters);
		}
	}
}