using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables.Enums;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Extensions;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseTaskReader : DatabaseReader, IDatabaseTaskReader {
		public DatabaseTaskReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public TaskProxy[] GetAll() {
			return ModelDatabase.Tasks.ToProxies();
		}

		public TaskProxy[] GetAllFromBoard(Guid boardId) {
			return ModelDatabase.Tasks.Where(task => task.BoardId == boardId).ToProxies();
		}

		public TaskProxy[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId) {
			IQueryable<Task> tasks = ModelDatabase.Tasks;
			UseFilter(header != null, ref tasks, task => task.Header.Contains(header));
			UseFilter(description != null, ref tasks, task => task.Description.Contains(description));
			UseFilter(branch != null, ref tasks, task => task.Branch.Contains(branch));
			UseFilter(state != null, ref tasks, task => task.State == state);
			UseFilter(priority != null, ref tasks, task => task.Priority == priority);
			UseFilter(developerId != null, ref tasks, task => task.DeveloperId == developerId);
			UseFilter(reviewerId != null, ref tasks, task => task.ReviewerId == reviewerId);
			UseFilter(columnId != null, ref tasks, task => task.ColumnId == columnId);
			UseFilter(boardId != null, ref tasks, task => task.BoardId == boardId);

			return tasks.ToProxies();
		}
	}
}