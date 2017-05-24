using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseColumnEditor : DatabaseEditor, IDatabaseColumnEditor {
		public DatabaseColumnEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(ColumnProxy proxy) {
			ModelDatabase.Columns.Add(new Column {
				ColumnId = Guid.NewGuid(),
				Header = proxy.Header,
				Board = ModelDatabase.GetBoard(proxy.BoardId)
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldProxyId, ColumnProxy newProxy) {
			var column = ModelDatabase.GetColumn(oldProxyId);
			column.Header = newProxy.Header;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid proxyId) {
			DeleteColumn(proxyId);
			ModelDatabase.SaveChanges();
		}
	}
}