using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.UserEdits {
	public class DeleteUserFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "DeleteUser";
		protected override AccessType RequiredAccessType => AccessType.AdminWrite;
		private readonly IDatabaseUserEditor databaseUserEditor;

		public DeleteUserFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserEditor databaseUserEditor) : base(databaseAuthorizer) {
			this.databaseUserEditor = databaseUserEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseUserEditor.Delete(parameters[HttpParameters.UserId].ToGuid());
		}
	}
}