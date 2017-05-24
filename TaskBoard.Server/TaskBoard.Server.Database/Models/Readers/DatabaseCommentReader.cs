using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Extensions;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseCommentReader : DatabaseReader, IDatabaseCommentReader {
		public DatabaseCommentReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public CommentProxy[] GetAll() {
			return ModelDatabase.Comments.ToProxies();
		}

		public CommentProxy[] GetAllFromTask(Guid taskId) {
			return ModelDatabase.Comments.Where(comment => comment.TaskId == taskId).ToProxies();
		}

		public CommentProxy[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			IQueryable<Comment> comments = ModelDatabase.Comments;
			UseFilter(content != null, ref comments, comment => comment.Content.Contains(content));
			UseFilter(beginCreateDateTime != null, ref comments, comment => comment.CreateDateTime >= beginCreateDateTime);
			UseFilter(endCreateDateTime != null, ref comments, comment => comment.CreateDateTime <= endCreateDateTime);
			UseFilter(userId != null, ref comments, comment => comment.UserId == userId);
			UseFilter(taskId != null, ref comments, comment => comment.TaskId == taskId);

			return comments.ToProxies();
		}
	}
}