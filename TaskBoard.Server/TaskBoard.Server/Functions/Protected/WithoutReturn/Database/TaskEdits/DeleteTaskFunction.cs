using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.TaskEdits {
	public class DeleteTaskFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "DeleteTask";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseTaskEditor databaseTaskEditor;

		public DeleteTaskFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskEditor databaseTaskEditor) : base(databaseAuthorizer) {
			this.databaseTaskEditor = databaseTaskEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseTaskEditor.Delete(parameters[HttpParameters.TaskId].ToGuid());
		}
	}
}