using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Extensions {
	public static class CommentExtensions {
		public static Comment[] ToTables(this IEnumerable<CommentEntity> comments) {
			return comments.Select(ToTable).ToArray();
		}
		public static Comment ToTable(this CommentEntity comment) {
			return new Comment {
				CommentId = comment.CommentId,
				Content = comment.Content,
				CreateDateTime = comment.CreateDateTime,
				UserId = comment.UserId,
				TaskId = comment.TaskId
			};
		}
	}
}