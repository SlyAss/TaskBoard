using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Extensions {
	// ReSharper disable MemberCanBePrivate.Global

	public static class ColumnExtensions {
		public static Column[] ToTables(this IEnumerable<ColumnEntity> columns) {
			return columns.Select(ToTable).ToArray();
		}
		public static Column ToTable(this ColumnEntity column) {
			return new Column {
				ColumnId = column.ColumnId,
				Header = column.Header,
				Brush = column.Brush,
				BoardId = column.BoardId
			};
		}
	}
}