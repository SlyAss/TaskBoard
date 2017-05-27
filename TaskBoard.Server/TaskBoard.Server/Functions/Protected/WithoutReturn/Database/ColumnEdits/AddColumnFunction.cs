using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.ColumnEdits {
	public class AddColumnFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "AddColumn";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseColumnEditor databaseColumnEditor;

		public AddColumnFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnEditor databaseColumnEditor) : base(databaseAuthorizer) {
			this.databaseColumnEditor = databaseColumnEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseColumnEditor.Add(requestBody.FromJson<Column>());
		}
	}
}