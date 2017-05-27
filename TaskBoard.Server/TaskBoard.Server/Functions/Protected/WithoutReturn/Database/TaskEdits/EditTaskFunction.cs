using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.TaskEdits {
	// ReSharper disable UnusedMember.Global

	public class EditTaskFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "EditTask";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseTaskEditor databaseTaskEditor;

		public EditTaskFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskEditor databaseTaskEditor) : base(databaseAuthorizer) {
			this.databaseTaskEditor = databaseTaskEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseTaskEditor.Edit(parameters[HttpParameters.TaskId].ToGuid(), requestBody.FromJson<Task>());
		}
	}
}