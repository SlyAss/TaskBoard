using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseColumnEditor : DatabaseEditor, IDatabaseColumnEditor {
		public DatabaseColumnEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(Column table) {
			ModelDatabase.Columns.Add(new ColumnEntity {
				ColumnId = Guid.NewGuid(),
				Header = table.Header,
				Board = ModelDatabase.GetBoard(table.BoardId)
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldTableId, Column newTable) {
			var column = ModelDatabase.GetColumn(oldTableId);
			column.Header = newTable.Header;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid tableId) {
			DeleteColumn(tableId);
			ModelDatabase.SaveChanges();
		}
	}
}