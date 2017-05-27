using System.Linq;
using TaskBoard.Common.Database;
using TaskBoard.Common.Enums;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models {
	public class DatabaseAuthorizer : IDatabaseAuthorizer {
		private readonly ModelDatabase modelDatabase;

		public DatabaseAuthorizer(ModelDatabase modelDatabase) {
			this.modelDatabase = modelDatabase;

			if (!modelDatabase.Users.Any())
				AddFirstUser();
		}
		private void AddFirstUser() {
			modelDatabase.Users.Add(new UserEntity {
				Login = "login",
				Password = "password",
				AccessType = int.MaxValue
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