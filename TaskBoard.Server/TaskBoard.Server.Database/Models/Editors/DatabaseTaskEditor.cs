using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models.Editors {
	// ReSharper disable UnusedMember.Global

	public class DatabaseTaskEditor : DatabaseEditor, IDatabaseTaskEditor {
		public DatabaseTaskEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(Task table) {
			ModelDatabase.Tasks.Add(new TaskEntity {
				TaskId = Guid.NewGuid(),
				Header = table.Header,
				Description = table.Description,
				Branch = table.Branch,
				State = table.State,
				Priority = table.Priority,
				CreateDateTime = DateTime.Now,
				Developer = ModelDatabase.GetUser(table.DeveloperId),
				Reviewer = ModelDatabase.GetUser(table.ReviewerId),
				Column = ModelDatabase.GetColumn(table.ColumnId),
				Board = ModelDatabase.GetBoard(table.BoardId)
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldTableId, Task newTable) {
			var task = ModelDatabase.GetTask(oldTableId);
			task.Header = newTable.Header;
			task.Description = newTable.Description;
			task.Branch = newTable.Branch;
			task.State = newTable.State;
			task.Priority = newTable.Priority;
			task.DeveloperId = ModelDatabase.GetUser(newTable.DeveloperId)?.UserId;
			task.ReviewerId = ModelDatabase.GetUser(newTable.ReviewerId)?.UserId;
			task.ColumnId = ModelDatabase.GetColumn(newTable.ColumnId)?.ColumnId;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid tableId) {
			DeleteTask(tableId);
			ModelDatabase.SaveChanges();
		}
	}
}