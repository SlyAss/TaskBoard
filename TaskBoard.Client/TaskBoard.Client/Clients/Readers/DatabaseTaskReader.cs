using System;
using System.Collections.Generic;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseTaskReader : BaseHttpClient, IDatabaseTaskReader {
		public DatabaseTaskReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public Task GetById(Guid id) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskId] = id.ToString();

			return SendRequest<Task>("GetTaskById", parameters);
		}

		public Guid[] GetAllIds() {
			return SendRequest<Guid[]>("GetAllTaskIds", GetDefaultParameters());
		}

		public Task[] GetAll() {
			return SendRequest<Task[]>("GetAllTasks", GetDefaultParameters());
		}

		public Guid[] GetIdsFromBoard(Guid boardId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskBoardId] = boardId.ToString();

			return SendRequest<Guid[]>("GetTaskIdsFromBoard", parameters);
		}

		public Task[] GetFromBoard(Guid boardId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskBoardId] = boardId.ToString();

			return SendRequest<Task[]>("GetTasksFromBoard", parameters);
		}

		public Guid[] GetIdsWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			return SendRequest<Guid[]>("GetTaskIdsWithUsingFilters", CreateParametersForUsingFilters(header, description, branch, state, priority, developerId, reviewerId, columnId, boardId));
		}

		public Task[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			return SendRequest<Task[]>("GetTasksWithUsingFilters", CreateParametersForUsingFilters(header, description, branch, state, priority, developerId, reviewerId, columnId, boardId));
		}

		private Dictionary<string, string> CreateParametersForUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskHeader, header);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskDescription, description);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskBranch, branch);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskState, state?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskPriority, priority?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskDeveloperId, developerId?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskReviewerId, reviewerId?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskColumnId, columnId?.ToString());
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.TaskBoardId, boardId?.ToString());

			return parameters;
		}
	}
}