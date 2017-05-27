using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Extensions {
	public static class TaskExtensions {
		public static Task[] ToTables(this IEnumerable<TaskEntity> tasks) {
			return tasks.Select(ToTable).ToArray();
		}
		public static Task ToTable(this TaskEntity task) {
			return new Task {
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