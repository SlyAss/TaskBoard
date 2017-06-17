using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Server.Database.Entities {
	// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
	// ReSharper disable UnusedAutoPropertyAccessor.Global

	public class CommentEntity {
		[Key]
		public Guid CommentId { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTime CreateDateTime { get; set; }

		[Required]
		public Guid UserId { get; set; }
		[Required, ForeignKey(nameof(UserId))]
		public virtual UserEntity User { get; set; }

		[Required]
		public Guid TaskId { get; set; }
		[Required, ForeignKey(nameof(TaskId))]
		public virtual TaskEntity Task { get; set; }
	}
}