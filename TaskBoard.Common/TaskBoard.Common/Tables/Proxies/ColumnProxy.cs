using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables.Proxies {
	public class ColumnProxy {
		[HeaderColumn("Id столбца")]
		public Guid ColumnId { get; set; }

		[HeaderColumn("Заголовок")]
		public string Header { get; set; }

		[HeaderColumn("Id доски")]
		public Guid BoardId { get; set; }
	}
}