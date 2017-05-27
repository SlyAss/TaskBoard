using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	// ReSharper disable UnusedMember.Global

	public class DatabaseUserReader : DatabaseReader, IDatabaseUserReader {
		public DatabaseUserReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public User[] GetAll() {
			return ModelDatabase.Users.ToTables();
		}

		public User[] GetWithUsingFilters(string login) {
			IQueryable<UserEntity> users = ModelDatabase.Users;
			UseFilter(login != null, ref users, user => user.Login.Contains(login));

			return users.ToTables();
		}
	}
}