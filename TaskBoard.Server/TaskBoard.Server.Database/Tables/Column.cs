using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Server.Database.Tables {
	public class Column {
		[Key]
		public Guid ColumnId { get; set; }

		[Required, MaxLength(64), Index("IX_ColumnUniques", 1, IsUnique = true)]
		public string Header { get; set; }

		[Required, Index("IX_ColumnUniques", 2, IsUnique = true)]
		public int BoardId { get; set; }

		[Required]
		public Board Board { get; set; }
	}
}