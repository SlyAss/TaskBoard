using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.BoardReads {
	public class GetAllBoardIdsFunction : HttpProtectedFunctionWithReturn<Guid[]> {
		public override string NameOfCalledMethod => "GetAllBoardIds";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseBoardReader databaseBoardReader;

		public GetAllBoardIdsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseBoardReader databaseBoardReader) : base(databaseAuthorizer) {
			this.databaseBoardReader = databaseBoardReader;
		}

		protected override Guid[] Run(NameValues parameters, byte[] requestBody) {
			return databaseBoardReader.GetAllIds();
		}
	}
}