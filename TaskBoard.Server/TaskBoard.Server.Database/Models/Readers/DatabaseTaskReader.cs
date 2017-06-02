using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	// ReSharper disable UnusedMember.Global

	public class DatabaseTaskReader : DatabaseReader, IDatabaseTaskReader {
		public DatabaseTaskReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Task GetById(Guid id) {
			return ModelDatabase.GetTask(id).ToTable();
		}

		public Guid[] GetAllIds() {
			return ModelDatabase.Tasks.Select(task => task.TaskId).ToArray();
		}

		public Task[] GetAll() {
			return ModelDatabase.Tasks.ToTables();
		}

		public Guid[] GetIdsFromBoard(Guid boardId) {
			return ModelDatabase.Tasks.Where(task => task.BoardId == boardId).Select(task => task.TaskId).ToArray();
		}

		public Task[] GetFromBoard(Guid boardId) {
			return ModelDatabase.Tasks.Where(task => task.BoardId == boardId).ToTables();
		}

		public Guid[] GetIdsWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			return GetQueryWithUsingFilters(header, description, branch, state, priority, developerId, reviewerId, columnId, boardId).Select(task => task.TaskId).ToArray();
		}

		public Task[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			return GetQueryWithUsingFilters(header, description, branch, state, priority, developerId, reviewerId, columnId, boardId).ToTables();
		}

		private IQueryable<TaskEntity> GetQueryWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			IQueryable<TaskEntity> tasks = ModelDatabase.Tasks;
			UseFilter(header != null, ref tasks, task => task.Header.Contains(header));
			UseFilter(description != null, ref tasks, task => task.Description.Contains(description));
			UseFilter(branch != null, ref tasks, task => task.Branch.Contains(branch));
			UseFilter(state != null, ref tasks, task => task.State == state);
			UseFilter(priority != null, ref tasks, task => task.Priority == priority);
			UseFilter(developerId != null, ref tasks, task => task.DeveloperId == developerId);
			UseFilter(reviewerId != null, ref tasks, task => task.ReviewerId == reviewerId);
			UseFilter(columnId != null, ref tasks, task => task.ColumnId == columnId);
			UseFilter(boardId != null, ref tasks, task => task.BoardId == boardId);

			return tasks;
		}
	}
}