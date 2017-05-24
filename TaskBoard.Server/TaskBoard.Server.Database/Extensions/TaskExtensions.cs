using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Extensions {
	public static class TaskExtensions {
		public static TaskProxy[] ToProxies(this IEnumerable<Task> tasks) {
			return tasks.Select(ToProxy).ToArray();
		}
		public static TaskProxy ToProxy(this Task task) {
			return new TaskProxy {
				TaskId = task.TaskId,
				Header = task.Header,
				Description = task.Description,
				Branch = task.Branch,
				State = task.State,
				Priority = task.Priority,
				CreateDateTime = task.CreateDateTime,
				DeveloperId = task.DeveloperId,
				ReviewerId = task.ReviewerId,
				ColumnId = task.ColumnId,
				BoardId = task.BoardId
			};
		}
	}
}