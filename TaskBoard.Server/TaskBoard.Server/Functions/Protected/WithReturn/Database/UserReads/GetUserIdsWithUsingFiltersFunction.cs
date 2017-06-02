using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.UserReads {
	public class GetUserIdsWithUsingFiltersFunction : HttpProtectedFunctionWithReturn<Guid[]> {
		public override string NameOfCalledMethod => "GetUserIdsWithUsingFilters";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseUserReader databaseUserReader;

		public GetUserIdsWithUsingFiltersFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserReader databaseUserReader) : base(databaseAuthorizer) {
			this.databaseUserReader = databaseUserReader;
		}

		protected override Guid[] Run(NameValues parameters, byte[] requestBody) {
			var login = parameters.GetValueOrNull(HttpParameters.UserLogin);

			return databaseUserReader.GetIdsWithUsingFilters(login);
		}
	}
}