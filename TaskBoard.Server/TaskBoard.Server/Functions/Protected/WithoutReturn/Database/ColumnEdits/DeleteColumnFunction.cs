using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.ColumnEdits {
	public class DeleteColumnFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "DeleteColumn";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseColumnEditor databaseColumnEditor;

		public DeleteColumnFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnEditor databaseColumnEditor) : base(databaseAuthorizer) {
			this.databaseColumnEditor = databaseColumnEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseColumnEditor.Delete(parameters[HttpParameters.ColumnId].ToGuid());
		}
	}
}