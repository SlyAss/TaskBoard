using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables.Proxies {
	public class CommentProxy {
		[HeaderColumn("Id комментария")]
		public Guid Commentid { get; set; }

		[HeaderColumn("Содержание")]
		public string Content { get; set; }

		[HeaderColumn("Дата создания")]
		public DateTime CreateDateTime { get; set; }

		[HeaderColumn("Id пользователя")]
		public Guid UserId { get; set; }

		[HeaderColumn("Id задачи")]
		public Guid TaskId { get; set; }
	}
}