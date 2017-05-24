using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Extensions {
	public static class CommentExtensions {
		public static CommentProxy[] ToProxies(this IEnumerable<Comment> comments) {
			return comments.Select(ToProxy).ToArray();
		}
		public static CommentProxy ToProxy(this Comment comment) {
			return new CommentProxy {
				CommentId = comment.CommentId,
				Content = comment.Content,
				CreateDateTime = comment.CreateDateTime,
				UserId = comment.UserId,
				TaskId = comment.TaskId
			};
		}
	}
}