using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.ColumnEdits {
	// ReSharper disable UnusedMember.Global

	public class EditColumnFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "EditColumn";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseColumnEditor databaseColumnEditor;

		public EditColumnFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnEditor databaseColumnEditor) : base(databaseAuthorizer) {
			this.databaseColumnEditor = databaseColumnEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseColumnEditor.Edit(parameters[HttpParameters.ColumnId].ToGuid(), requestBody.FromJson<Column>());
		}
	}
}