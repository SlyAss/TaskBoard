using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Server.Database.Tables {
	public class Comment {
		[Key]
		public Guid CommentId { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTime CreateDateTime { get; set; }

		[Required]
		public Guid UserId { get; set; }

		[Required]
		public virtual User User { get; set; }

		[Required]
		public Guid TaskId { get; set; }

		[Required]
		public virtual Task Task { get; set; }
	}
}