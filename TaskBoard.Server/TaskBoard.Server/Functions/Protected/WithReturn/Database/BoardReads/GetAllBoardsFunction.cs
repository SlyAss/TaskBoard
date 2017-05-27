using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.BoardReads {
	// ReSharper disable UnusedMember.Global

	public class GetAllBoardsFunction : HttpProtectedFunctionWithReturn<Board[]> {
		public override string NameOfCalledMethod => "GetAllBoards";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseBoardReader databaseBoardReader;

		public GetAllBoardsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseBoardReader databaseBoardReader) : base(databaseAuthorizer) {
			this.databaseBoardReader = databaseBoardReader;
		}

		protected override Board[] Run(NameValues parameters, byte[] requestBody) {
			return databaseBoardReader.GetAll();
		}
	}
}