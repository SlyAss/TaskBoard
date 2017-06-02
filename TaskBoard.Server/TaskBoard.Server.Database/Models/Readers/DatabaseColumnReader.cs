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

		public Column GetById(Guid id) {
			return ModelDatabase.GetColumn(id).ToTable();
		}

		public Guid[] GetAllIds() {
			return ModelDatabase.Columns.Select(column => column.ColumnId).ToArray();
		}

		public Column[] GetAll() {
			return ModelDatabase.Columns.ToTables();
		}

		public Guid[] GetIdsFromBoard(Guid boardId) {
			return ModelDatabase.Columns.Where(column => column.BoardId == boardId).Select(column => column.ColumnId).ToArray();
		}

		public Column[] GetFromBoard(Guid boardId) {
			return ModelDatabase.Columns.Where(column => column.BoardId == boardId).ToTables();
		}

		public Guid[] GetIdsWithUsingFilters(string header, string brush, Guid? boardId) {
			return GetQueryWithUsingFilters(header, brush, boardId).Select(column => column.ColumnId).ToArray();
		}

		public Column[] GetWithUsingFilters(string header, string brush, Guid? boardId) {
			return GetQueryWithUsingFilters(header, brush, boardId).ToTables();
		}

		private IQueryable<ColumnEntity> GetQueryWithUsingFilters(string header, string brush, Guid? boardId) {
			IQueryable<ColumnEntity> columns = ModelDatabase.Columns;
			UseFilter(header != null, ref columns, column => column.Header.Contains(header));
			UseFilter(brush != null, ref columns, column => column.Brush == brush);
			UseFilter(boardId != null, ref columns, column => column.ColumnId == boardId.Value);

			return columns;
		}
	}
}