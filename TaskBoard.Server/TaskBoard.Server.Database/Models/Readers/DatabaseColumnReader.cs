using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Extensions;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseColumnReader : DatabaseReader, IDatabaseColumnReader {
		public DatabaseColumnReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public ColumnProxy[] GetAll() {
			return ModelDatabase.Columns.ToProxies();
		}

		public ColumnProxy[] GetWithUsingFilters(string header, Guid? boardId) {
			IQueryable<Column> columns = ModelDatabase.Columns;
			UseFilter(header != null, ref columns, column => column.Header.Contains(header));
			UseFilter(boardId != null, ref columns, column => column.ColumnId == boardId.Value);

			return columns.ToProxies();
		}
	}
}