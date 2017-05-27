using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseCommentReader : DatabaseReader, IDatabaseCommentReader {
		public DatabaseCommentReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Comment[] GetAll() {
			return ModelDatabase.Comments.ToTables();
		}

		public Comment[] GetAllFromTask(Guid taskId) {
			return ModelDatabase.Comments.Where(comment => comment.TaskId == taskId).ToTables();
		}

		public Comment[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			IQueryable<CommentEntity> comments = ModelDatabase.Comments;
			UseFilter(content != null, ref comments, comment => comment.Content.Contains(content));
			UseFilter(beginCreateDateTime != null, ref comments, comment => comment.CreateDateTime >= beginCreateDateTime);
			UseFilter(endCreateDateTime != null, ref comments, comment => comment.CreateDateTime <= endCreateDateTime);
			UseFilter(userId != null, ref comments, comment => comment.UserId == userId);
			UseFilter(taskId != null, ref comments, comment => comment.TaskId == taskId);

			return comments.ToTables();
		}
	}
}