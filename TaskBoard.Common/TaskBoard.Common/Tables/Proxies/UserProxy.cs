using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables.Proxies {
	public class UserProxy {
		[HeaderColumn("Id пользователя")]
		public Guid UserId { get; set; }

		[HeaderColumn("Логин")]
		public string Login { get; set; }

		[HeaderColumn("Пароль")]
		public string Password { get; set; }

		[HeaderColumn("Тип доступа")]
		public int AccessType { get; set; }
	}
}