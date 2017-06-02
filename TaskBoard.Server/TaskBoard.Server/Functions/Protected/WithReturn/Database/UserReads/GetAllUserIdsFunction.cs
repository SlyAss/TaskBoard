using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.UserReads {
	public class GetAllUserIdsFunction : HttpProtectedFunctionWithReturn<Guid[]> {
		public override string NameOfCalledMethod => "GetAllUserIds";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseUserReader databaseUserReader;

		public GetAllUserIdsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserReader databaseUserReader) : base(databaseAuthorizer) {
			this.databaseUserReader = databaseUserReader;
		}

		protected override Guid[] Run(NameValues parameters, byte[] requestBody) {
			return databaseUserReader.GetAllIds();
		}
	}
}