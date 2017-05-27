using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.TaskEdits {
	public class AddTaskFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "AddTask";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseTaskEditor databaseTaskEditor;

		public AddTaskFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseTaskEditor databaseTaskEditor) : base(databaseAuthorizer) {
			this.databaseTaskEditor = databaseTaskEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseTaskEditor.Add(requestBody.FromJson<Task>());
		}
	}
}