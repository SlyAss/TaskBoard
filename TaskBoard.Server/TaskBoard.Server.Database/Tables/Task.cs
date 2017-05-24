using System;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Common.Tables.Enums;

namespace TaskBoard.Server.Database.Tables {
	public class Task {
		[Key]
		public Guid Taskid { get; set; }

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
		public Guid DeveloperId { get; set; }

		[Required]
		public virtual User Developer { get; set; }

		[Required]
		public Guid ReviewerId { get; set; }

		[Required]
		public virtual User Reviewer { get; set; }

		[Required]
		public Guid ColumnId { get; set; }

		[Required]
		public virtual Column Column { get; set; }

		[Required]
		public Guid BoardId { get; set; }

		[Required]
		public virtual Board Board { get; set; }
	}
}