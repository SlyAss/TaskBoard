using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseUserReaderAsAdmin : DatabaseReader, IDatabaseUserReaderAsAdmin {
		public DatabaseUserReaderAsAdmin(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public User GetById(Guid id) {
			return ModelDatabase.GetUser(id).ToTableAsAdmin();
		}

		public Guid[] GetAllIds() {
			return ModelDatabase.Users.Select(user => user.UserId).ToArray();
		}

		public User[] GetAll() {
			return ModelDatabase.Users.ToTablesAsAdmin();
		}

		public Guid[] GetIdsWithUsingFilters(string login) {
			return GetQueryWithUsingFilters(login).Select(user => user.UserId).ToArray();
		}

		public User[] GetWithUsingFilters(string login) {
			return GetQueryWithUsingFilters(login).ToTablesAsAdmin();
		}

		private IQueryable<UserEntity> GetQueryWithUsingFilters(string login) {
			IQueryable<UserEntity> users = ModelDatabase.Users;
			UseFilter(login != null, ref users, user => user.Login.Contains(login));

			return users;
		}
	}
}