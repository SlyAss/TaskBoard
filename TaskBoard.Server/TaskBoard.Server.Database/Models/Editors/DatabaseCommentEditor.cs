using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseCommentEditor : DatabaseEditor, IDatabaseCommentEditor {
		public DatabaseCommentEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(CommentProxy proxy) {
			ModelDatabase.Comments.Add(new Comment {
				CommentId = Guid.NewGuid(),
				Content = proxy.Content,
				CreateDateTime = DateTime.Now,
				User = ModelDatabase.GetUser(proxy.UserId),
				Task = ModelDatabase.GetTask(proxy.TaskId)
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldProxyId, CommentProxy newProxy) {
			var comment = ModelDatabase.GetComment(oldProxyId);
			comment.Content = newProxy.Content;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid proxyId) {
			DeleteComment(proxyId);
			ModelDatabase.SaveChanges();
		}
	}
}