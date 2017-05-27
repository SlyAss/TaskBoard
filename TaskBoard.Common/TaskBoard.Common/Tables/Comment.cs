using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables {
	// ReSharper disable ClassNeverInstantiated.Global
	// ReSharper disable UnusedMember.Global

	public class Comment {
		[HeaderColumn("Id комментария")]
		public Guid CommentId { get; set; }

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