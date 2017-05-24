using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Extensions;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseUserReader : DatabaseReader, IDatabaseUserReader {
		public DatabaseUserReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public UserProxy[] GetAll() {
			return ModelDatabase.Users.ToProxies();
		}

		public UserProxy[] GetWithUsingFilters(string login, string password) {
			IQueryable<User> users = ModelDatabase.Users;
			UseFilter(login != null, ref users, user => user.Login.Contains(login));
			UseFilter(password != null, ref users, user => user.Password.Contains(password));

			return users.ToProxies();
		}
	}
}