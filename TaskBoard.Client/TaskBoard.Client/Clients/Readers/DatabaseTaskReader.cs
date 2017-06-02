using System;
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

		public Task[] GetAll() {
			return SendRequest<Task[]>("GetAllTasks", GetDefaultParameters());
		}

		public Task[] GetFromBoard(Guid boardId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.TaskBoardId] = boardId.ToString();

			return SendRequest<Task[]>("GetTasksFromBoard", parameters);
		}

		public Task[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
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

			return SendRequest<Task[]>("GetTasksWithUsingFilters", parameters);
		}
	}
}