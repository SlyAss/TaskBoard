using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables {
	// ReSharper disable ClassNeverInstantiated.Global
	// ReSharper disable UnusedMember.Global

	public class Column {
		[HeaderColumn("Id столбца")]
		public Guid ColumnId { get; set; }

		[HeaderColumn("Заголовок")]
		public string Header { get; set; }

		[HeaderColumn("Цвет задач")]
		public string Brush { get; set; }

		[HeaderColumn("Id доски")]
		public Guid BoardId { get; set; }
	}
}