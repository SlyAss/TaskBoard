using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.TaskReads {
	public class GetAllTasksFromBoardFunction : HttpProtectedFunctionWithReturn<Task[]> {
		public override string NameOfCalledMethod => "GetAllTasksFromBoard";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseTaskReader databaseTaskReader;

		public GetAllTasksFromBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskReader databaseTaskReader) : base(databaseAuthorizer) {
			this.databaseTaskReader = databaseTaskReader;
		}

		protected override Task[] Run(NameValues parameters, byte[] requestBody) {
			var boardId = parameters[HttpParameters.BoardId].ToGuid();

			return databaseTaskReader.GetAllFromBoard(boardId);
		}
	}
}