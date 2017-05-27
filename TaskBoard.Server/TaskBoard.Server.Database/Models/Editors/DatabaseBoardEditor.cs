using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseBoardEditor : DatabaseEditor, IDatabaseBoardEditor {
		public DatabaseBoardEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(Board table) {
			ModelDatabase.Boards.Add(new BoardEntity {
				BoardId = Guid.NewGuid(),
				Name = table.Name
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldTableId, Board newTable) {
			var board = ModelDatabase.GetBoard(oldTableId);
			board.Name = newTable.Name;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid tableId) {
			DeleteBoard(tableId);
			ModelDatabase.SaveChanges();
		}
	}
}