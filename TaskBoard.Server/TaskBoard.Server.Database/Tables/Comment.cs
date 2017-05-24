using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Server.Database.Tables {
	public class Comment {
		[Key]
		public Guid Commentid { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTime CreateDateTime { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public virtual User User { get; set; }

		[Required]
		public int TaskId { get; set; }

		[Required]
		public virtual Task Task { get; set; }
	}
}