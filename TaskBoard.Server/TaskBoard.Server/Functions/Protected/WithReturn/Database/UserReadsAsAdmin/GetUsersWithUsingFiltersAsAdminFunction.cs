using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.UserReadsAsAdmin {
	// ReSharper disable UnusedMember.Global

	public class GetUsersWithUsingFiltersAsAdminFunction : HttpProtectedFunctionWithReturn<User[]> {
		public override string NameOfCalledMethod => "GetUsersWithUsingFiltersAsAdmin";
		protected override AccessType RequiredAccessType => AccessType.AdminRead;
		private readonly IDatabaseUserReaderAsAdmin databaseUserReaderAsAdmin;

		public GetUsersWithUsingFiltersAsAdminFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserReaderAsAdmin databaseUserReaderAsAdmin) : base(databaseAuthorizer) {
			this.databaseUserReaderAsAdmin = databaseUserReaderAsAdmin;
		}

		protected override User[] Run(NameValues parameters, byte[] requestBody) {
			var login = parameters.GetValueOrNull(HttpParameters.UserLogin);

			return databaseUserReaderAsAdmin.GetWithUsingFilters(login);
		}
	}
}