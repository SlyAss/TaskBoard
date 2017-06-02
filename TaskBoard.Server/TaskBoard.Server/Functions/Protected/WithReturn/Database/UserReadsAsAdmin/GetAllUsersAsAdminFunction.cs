using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.UserReadsAsAdmin {
	// ReSharper disable UnusedMember.Global

	public class GetAllUsersAsAdminFunction : HttpProtectedFunctionWithReturn<User[]> {
		public override string NameOfCalledMethod => "GetAllUsersAsAdmin";
		protected override AccessType RequiredAccessType => AccessType.AdminRead;
		private readonly IDatabaseUserReaderAsAdmin databaseUserReaderAsAdmin;

		public GetAllUsersAsAdminFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserReaderAsAdmin databaseUserReaderAsAdmin) : base(databaseAuthorizer) {
			this.databaseUserReaderAsAdmin = databaseUserReaderAsAdmin;
		}

		protected override User[] Run(NameValues parameters, byte[] requestBody) {
			return databaseUserReaderAsAdmin.GetAll();
		}
	}
}