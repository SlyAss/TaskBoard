using System.Net;
using TaskBoard.Common.Database;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;
using TaskBoard.Server.Exceptions;

namespace TaskBoard.Server.Functions.NonProtected.WithoutReturn {
	// ReSharper disable UnusedMember.Global

	public class CheckUserIsExistFunction : HttpFunctionWithoutReturn {
		public override string NameOfCalledMethod => "CheckUserIsExist";
		private readonly IDatabaseAuthorizer databaseAuthorizer;

		public CheckUserIsExistFunction(IDatabaseAuthorizer databaseAuthorizer) {
			this.databaseAuthorizer = databaseAuthorizer;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			var login = parameters.GetValueOrThrow(HttpParameters.Login, "Для вызова этой функции необходимо передать параметры пользователя");
			var password = parameters.GetValueOrThrow(HttpParameters.Password, "Для вызова этой функции необходимо передать параметры пользователя");

			if (!databaseAuthorizer.UserIsExist(login, password))
				throw new HttpException(HttpStatusCode.NotFound, "Пользователь не найден");
		}
	}
}