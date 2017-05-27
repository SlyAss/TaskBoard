using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.TaskReads {
	public class GetTasksWithUsingFiltersFunction : HttpProtectedFunctionWithReturn<Task[]> {
		public override string NameOfCalledMethod => "GetTasksWithUsingFilters";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseTaskReader databaseTaskReader;

		public GetTasksWithUsingFiltersFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskReader databaseTaskReader) : base(databaseAuthorizer) {
			this.databaseTaskReader = databaseTaskReader;
		}

		protected override Task[] Run(NameValues parameters, byte[] requestBody) {
			var header = parameters.GetValueOrNull(HttpParameters.TaskHeader);
			var description = parameters.GetValueOrNull(HttpParameters.TaskDescription);
			var branch = parameters.GetValueOrNull(HttpParameters.TaskBranch);
			var state = parameters.GetValueOrNull(HttpParameters.TaskState)?.ToState();
			var priority = parameters.GetValueOrNull(HttpParameters.TaskPriority)?.ToPriority();
			var developerId = parameters.GetValueOrNull(HttpParameters.TaskDeveloperId)?.ToGuid();
			var reviewerId = parameters.GetValueOrNull(HttpParameters.TaskReviewerId)?.ToGuid();
			var columnId = parameters.GetValueOrNull(HttpParameters.TaskColumnId)?.ToGuid();
			var boardId = parameters.GetValueOrNull(HttpParameters.TaskBoardId)?.ToGuid();

			return databaseTaskReader.GetWithUsingFilters(header, description, branch, state, priority, developerId, reviewerId, columnId, boardId);
		}
	}
}