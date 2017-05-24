using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Extensions {
	public static class UserExtensions {
		public static UserProxy[] ToProxies(this IEnumerable<User> users) {
			return users.Select(ToProxy).ToArray();
		}
		public static UserProxy ToProxy(this User user) {
			return new UserProxy {
				UserId = user.UserId,
				Login = user.Login,
				Password = user.Password,
				AccessType = user.AccessType
			};
		}
	}
}