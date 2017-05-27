using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Extensions {
	// ReSharper disable MemberCanBePrivate.Global

	public static class UserExtensions {
		public static User[] ToTables(this IEnumerable<UserEntity> users) {
			return users.Select(ToTable).ToArray();
		}
		public static User ToTable(this UserEntity user) {
			return new User {
				UserId = user.UserId,
				Login = user.Login,
				Password = user.Password,
				AccessType = user.AccessType
			};
		}
	}
}