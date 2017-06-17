using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskBoard.Common.Enums;

namespace TaskBoard.Server.Database.Entities {
	// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
	// ReSharper disable UnusedAutoPropertyAccessor.Global

	public class TaskEntity {
		[Key]
		public Guid TaskId { get; set; }

		[Required]
		public string Header { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Branch { get; set; }

		[Required]
		public State State { get; set; }

		[Required]
		public Priority Priority { get; set; }

		[Required]
		public DateTime CreateDateTime { get; set; }

		public Guid? DeveloperId { get; set; }
		[ForeignKey(nameof(DeveloperId))]
		public virtual UserEntity Developer { get; set; }

		public Guid? ReviewerId { get; set; }
		[ForeignKey(nameof(ReviewerId))]
		public virtual UserEntity Reviewer { get; set; }

		public Guid? ColumnId { get; set; }
		[ForeignKey(nameof(ColumnId))]
		public virtual ColumnEntity Column { get; set; }

		[Required]
		public Guid BoardId { get; set; }
		[Required, ForeignKey(nameof(BoardId))]
		public virtual BoardEntity Board { get; set; }
	}
}