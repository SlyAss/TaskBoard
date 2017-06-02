using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.TaskReads {
	// ReSharper disable UnusedMember.Global

	public class GetTasksFromColumnFunction : HttpProtectedFunctionWithReturn<Task[]> {
		public override string NameOfCalledMethod => "GetTasksFromColumn";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseTaskReader databaseTaskReader;

		public GetTasksFromColumnFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskReader databaseTaskReader) : base(databaseAuthorizer) {
			this.databaseTaskReader = databaseTaskReader;
		}

		protected override Task[] Run(NameValues parameters, byte[] requestBody) {
			return databaseTaskReader.GetFromColumn(parameters[HttpParameters.TaskBoardId].ToGuid(), parameters[HttpParameters.TaskColumnId].ToGuid());
		}
	}
}