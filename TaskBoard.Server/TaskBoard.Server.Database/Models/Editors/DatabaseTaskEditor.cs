using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseTaskEditor : DatabaseEditor, IDatabaseTaskEditor {
		public DatabaseTaskEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(TaskProxy proxy) {
			ModelDatabase.Tasks.Add(new Task {
				TaskId = Guid.NewGuid(),
				Header = proxy.Header,
				Description = proxy.Description,
				Branch = proxy.Branch,
				State = proxy.State,
				Priority = proxy.Priority,
				CreateDateTime = DateTime.Now,
				Developer = ModelDatabase.GetUser(proxy.DeveloperId),
				Reviewer = ModelDatabase.GetUser(proxy.ReviewerId),
				Column = ModelDatabase.GetColumn(proxy.ColumnId),
				Board = ModelDatabase.GetBoard(proxy.BoardId)
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldProxyId, TaskProxy newProxy) {
			var task = ModelDatabase.GetTask(oldProxyId);
			task.Header = newProxy.Header;
			task.Description = newProxy.Description;
			task.Branch = newProxy.Branch;
			task.State = newProxy.State;
			task.Priority = newProxy.Priority;
			task.Developer = ModelDatabase.GetUser(newProxy.DeveloperId);
			task.Reviewer = ModelDatabase.GetUser(newProxy.ReviewerId);
			task.Column = ModelDatabase.GetColumn(newProxy.ColumnId);

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid proxyId) {
			DeleteTask(proxyId);
			ModelDatabase.SaveChanges();
		}
	}
}