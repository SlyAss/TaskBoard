using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseUserEditor : DatabaseEditor, IDatabaseUserEditor {
		public DatabaseUserEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(User table) {
			ModelDatabase.Users.Add(new UserEntity {
				UserId = Guid.NewGuid(),
				Login = table.Login,
				Password = table.Password,
				AccessType = table.AccessType
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldTableId, User newTable) {
			var user = ModelDatabase.GetUser(oldTableId);
			user.Login = newTable.Login;
			user.Password = newTable.Password;
			user.AccessType = newTable.AccessType;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid tableId) {
			DeleteUser(tableId);
			ModelDatabase.SaveChanges();
		}
	}
}