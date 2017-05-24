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
		public int DeveloperId { get; set; }

		[Required]
		public User Developer { get; set; }

		[Required]
		public int ReviewerId { get; set; }

		[Required]
		public User Reviewer { get; set; }

		[Required]
		public int ColumnId { get; set; }

		[Required]
		public Column Column { get; set; }

		[Required]
		public int BoardId { get; set; }

		[Required]
		public Board Board { get; set; }
	}
}