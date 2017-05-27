using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.UserEdits {
	public class EditUserFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "EditUser";
		protected override AccessType RequiredAccessType => AccessType.AdminWrite;
		private readonly IDatabaseUserEditor databaseUserEditor;

		public EditUserFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseUserEditor databaseUserEditor) : base(databaseAuthorizer) {
			this.databaseUserEditor = databaseUserEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseUserEditor.Edit(parameters[HttpParameters.UserId].ToGuid(), requestBody.FromJson<User>());
		}
	}
}