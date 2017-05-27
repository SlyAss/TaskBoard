using System;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseCommentReader : BaseHttpClient, IDatabaseCommentReader {
		public DatabaseCommentReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public Comment[] GetAll() {
			return SendRequest<Comment[]>("GetAllComments", GetDefaultParameters());
		}

		public Comment[] GetFromTask(Guid taskId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentTaskId] = taskId.ToString();

			return SendRequest<Comment[]>("GetCommentsFromTask", parameters);
		}

		public Comment[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentContent, content);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentBeginCreateDateTime, beginCreateDateTime?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentEndCreateDateTime, endCreateDateTime?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentUserId, userId?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentTaskId, taskId?.ToString());

			return SendRequest<Comment[]>("GetCommentsWithUsingFilters", parameters);
		}
	}
}