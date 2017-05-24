using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseBoardEditor : DatabaseEditor, IDatabaseBoardEditor {
		public DatabaseBoardEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(BoardProxy proxy) {
			ModelDatabase.Boards.Add(new Board {
				BoardId = Guid.NewGuid(),
				Name = proxy.Name
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldProxyId, BoardProxy newProxy) {
			var board = ModelDatabase.GetBoard(oldProxyId);
			board.Name = newProxy.Name;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid proxyId) {
			DeleteBoard(proxyId);
			ModelDatabase.SaveChanges();
		}
	}
}