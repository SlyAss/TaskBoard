using System;
using System.Collections.Generic;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseCommentReader : BaseHttpClient, IDatabaseCommentReader {
		public DatabaseCommentReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public Comment GetById(Guid id) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentId] = id.ToString();

			return SendRequest<Comment>("GetCommentById", parameters);
		}

		public Guid[] GetAllIds() {
			return SendRequest<Guid[]>("GetAllCommentIds", GetDefaultParameters());
		}

		public Comment[] GetAll() {
			return SendRequest<Comment[]>("GetAllComments", GetDefaultParameters());
		}

		public Guid[] GetIdsFromTask(Guid taskId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentTaskId] = taskId.ToString();

			return SendRequest<Guid[]>("GetCommentIdsFromTask", parameters);
		}

		public Comment[] GetFromTask(Guid taskId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.CommentTaskId] = taskId.ToString();

			return SendRequest<Comment[]>("GetCommentsFromTask", parameters);
		}

		public Guid[] GetIdsWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			return SendRequest<Guid[]>("GetCommentIdsWithUsingFilters", CreateParametersForUsingFilters(content, beginCreateDateTime, endCreateDateTime, userId, taskId));
		}

		public Comment[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			return SendRequest<Comment[]>("GetCommentsWithUsingFilters", CreateParametersForUsingFilters(content, beginCreateDateTime, endCreateDateTime, userId, taskId));
		}

		private Dictionary<string, string> CreateParametersForUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentContent, content);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentBeginCreateDateTime, beginCreateDateTime?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentEndCreateDateTime, endCreateDateTime?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentUserId, userId?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.CommentTaskId, taskId?.ToString());

			return parameters;
		}
	}
}