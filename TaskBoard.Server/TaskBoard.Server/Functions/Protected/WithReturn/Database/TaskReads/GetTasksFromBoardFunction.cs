using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.TaskReads {
	public class GetTasksFromBoardFunction : HttpProtectedFunctionWithReturn<Task[]> {
		public override string NameOfCalledMethod => "GetTasksFromBoard";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseTaskReader databaseTaskReader;

		public GetTasksFromBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskReader databaseTaskReader) : base(databaseAuthorizer) {
			this.databaseTaskReader = databaseTaskReader;
		}

		protected override Task[] Run(NameValues parameters, byte[] requestBody) {
			return databaseTaskReader.GetFromBoard(parameters[HttpParameters.TaskBoardId].ToGuid());
		}
	}
}