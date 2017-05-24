using System;
using System.Linq;

namespace TaskBoard.Server.Database.Models {
	public abstract class DatabaseEditor {
		protected readonly ModelDatabase ModelDatabase;

		protected DatabaseEditor(ModelDatabase modelDatabase) {
			ModelDatabase = modelDatabase;
		}

		protected void DeleteBoard(Guid boardId) {
			ModelDatabase.Tasks.Where(t => t.BoardId == boardId).ToList().ForEach(task => DeleteTask(task.TaskId));
			ModelDatabase.Columns.Where(c => c.BoardId == boardId).ToList().ForEach(column => DeleteColumn(column.ColumnId));
			ModelDatabase.Boards.Remove(ModelDatabase.GetBoard(boardId));
		}

		protected void DeleteColumn(Guid columnId) {
			ModelDatabase.Tasks.Where(t => t.ColumnId == columnId).ToList().ForEach(task => task.Column = null);
			ModelDatabase.Columns.Remove(ModelDatabase.GetColumn(columnId));
		}

		protected void DeleteComment(Guid commentId) {
			ModelDatabase.Comments.Remove(ModelDatabase.GetComment(commentId));
		}

		protected void DeleteTask(Guid taskId) {
			ModelDatabase.Comments.Where(comment => comment.TaskId == taskId).ToList().ForEach(comment => DeleteComment(comment.CommentId));
			ModelDatabase.Tasks.Remove(ModelDatabase.GetTask(taskId));
		}

		protected void DeleteUser(Guid userId) {
			ModelDatabase.Comments.Where(comment => comment.UserId == userId).ToList().ForEach(comment => DeleteComment(comment.CommentId));
			ModelDatabase.Tasks.Where(task => task.DeveloperId == userId).ToList().ForEach(task => task.DeveloperId = null);
			ModelDatabase.Tasks.Where(task => task.ReviewerId == userId).ToList().ForEach(task => task.ReviewerId = null);
			ModelDatabase.Users.Remove(ModelDatabase.GetUser(userId));
		}
	}
}