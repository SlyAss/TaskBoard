using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Extensions {
	public static class ColumnExtensions {
		public static ColumnProxy[] ToProxies(this IEnumerable<Column> columns) {
			return columns.Select(ToProxy).ToArray();
		}
		public static ColumnProxy ToProxy(this Column column) {
			return new ColumnProxy {
				ColumnId = column.ColumnId,
				Header = column.Header,
				BoardId = column.BoardId
			};
		}
	}
}