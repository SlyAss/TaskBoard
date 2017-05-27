using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Editors {
	public class DatabaseCommentEditor : BaseHttpClient, IDatabaseCommentEditor {
		public DatabaseCommentEditor(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public void Add(Comment table) {
			SendRequest("AddComment", GetDefaultParameters(), table.ToJson());
		}

		public void Edit(Guid oldTableId, Comment newTable) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentId] = oldTableId.ToString();

			SendRequest("EditComment", parameters, newTable.ToJson());
		}

		public void Delete(Guid tableId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentId] = tableId.ToString();

			SendRequest("DeleteComment", parameters);
		}
	}
}