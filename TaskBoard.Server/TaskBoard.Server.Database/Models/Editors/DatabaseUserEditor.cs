using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Editors {
	public class DatabaseUserEditor : DatabaseEditor, IDatabaseUserEditor {
		public DatabaseUserEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(UserProxy proxy) {
			ModelDatabase.Users.Add(new User {
				UserId = Guid.NewGuid(),
				Login = proxy.Login,
				Password = proxy.Password,
				AccessType = proxy.AccessType
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldProxyId, UserProxy newProxy) {
			var user = ModelDatabase.GetUser(oldProxyId);
			user.Login = newProxy.Login;
			user.Password = newProxy.Password;
			user.AccessType = newProxy.AccessType;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid proxyId) {
			DeleteUser(proxyId);
			ModelDatabase.SaveChanges();
		}
	}
}