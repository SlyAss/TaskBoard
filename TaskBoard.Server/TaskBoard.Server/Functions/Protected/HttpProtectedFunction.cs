﻿using System.Net;
using TaskBoard.Common.Database;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;
using TaskBoard.Server.Exceptions;

namespace TaskBoard.Server.Functions.Protected {
	public abstract class HttpProtectedFunction : IHttpFunction {
		public abstract string NameOfCalledMethod { get; }
		protected abstract AccessType RequiredAccessType { get; }
		private readonly IDatabaseAuthorizer databaseAuthorizer;

		protected HttpProtectedFunction(IDatabaseAuthorizer databaseAuthorizer) {
			this.databaseAuthorizer = databaseAuthorizer;
		}

		public void Execute(HttpListenerContext context, NameValues parameters, byte[] requestBody) {
			var login = parameters.GetValueOrThrow(HttpParameters.Login, "Для вызова этой функции необходимо передать параметры пользователя");
			var password = parameters.GetValueOrThrow(HttpParameters.Password, "Для вызова этой функции необходимо передать параметры пользователя");

			if (!databaseAuthorizer.UserIsExist(login, password))
				throw new HttpException(HttpStatusCode.NotFound, "Пользователь не найден, либо введен неверный пароль");
			if (!databaseAuthorizer.AccessIsAllowed(login, password, RequiredAccessType))
				throw new HttpException(HttpStatusCode.Forbidden, "У вас нет доступа к этой функции");
			PerformRun(context, parameters, requestBody);
		}
		protected abstract void PerformRun(HttpListenerContext context, NameValues parameters, byte[] requestBody);
	}
}