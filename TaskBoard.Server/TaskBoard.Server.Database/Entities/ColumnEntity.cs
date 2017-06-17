using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Server.Database.Entities {
	// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
	// ReSharper disable UnusedAutoPropertyAccessor.Global

	public class ColumnEntity {
		[Key]
		public Guid ColumnId { get; set; }

		[Required, MaxLength(64), Index("IX_ColumnUniques", 1, IsUnique = true)]
		public string Header { get; set; }

		[Required, MaxLength(10)]
		public string Brush { get; set; }

		[Required, Index("IX_ColumnUniques", 2, IsUnique = true)]
		public Guid BoardId { get; set; }
		[Required, ForeignKey(nameof(BoardId))]
		public virtual BoardEntity Board { get; set; }
	}
}