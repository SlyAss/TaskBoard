using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	// ReSharper disable UnusedMember.Global

	public class DatabaseColumnReader : DatabaseReader, IDatabaseColumnReader {
		public DatabaseColumnReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Column[] GetAll() {
			return ModelDatabase.Columns.ToTables();
		}

		public Column[] GetFromBoard(Guid boardId) {
			return ModelDatabase.Columns.Where(column => column.BoardId == boardId).ToTables();
		}

		public Column[] GetWithUsingFilters(string header, Guid? boardId) {
			IQueryable<ColumnEntity> columns = ModelDatabase.Columns;
			UseFilter(header != null, ref columns, column => column.Header.Contains(header));
			UseFilter(boardId != null, ref columns, column => column.ColumnId == boardId.Value);

			return columns.ToTables();
		}
	}
}