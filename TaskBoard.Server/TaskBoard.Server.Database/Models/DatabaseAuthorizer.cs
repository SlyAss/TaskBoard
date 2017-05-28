using System;
using System.Linq;
using TaskBoard.Common.Database;
using TaskBoard.Common.Enums;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models {
	// ReSharper disable UnusedMember.Global

	public class DatabaseAuthorizer : IDatabaseAuthorizer {
		private readonly ModelDatabase modelDatabase;

		public DatabaseAuthorizer(ModelDatabase modelDatabase) {
			this.modelDatabase = modelDatabase;

			if (!modelDatabase.Users.Any())
				AddFirstUser();
		}
		private void AddFirstUser() {
			modelDatabase.Users.Add(new UserEntity {
				UserId = Guid.NewGuid(),
				Login = "login",
				Password = "password",
				AccessType = -1
			});
			modelDatabase.SaveChanges();
		}

		public bool UserIsExist(string login, string password) {
			return modelDatabase.Users.FirstOrDefault(user => user.Login == login && user.Password == password) != null;
		}
		public bool AccessIsAllowed(string login, string password, AccessType requiredAccessType) {
			var foundUser = modelDatabase.Users.First(user => user.Login == login && user.Password == password);
			return -(foundUser.AccessType | (-(int)requiredAccessType - 1)) - 1 == 0;
		}
	}
}