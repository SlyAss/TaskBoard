using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.BoardEdits {
	public class DeleteBoardFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "DeleteBoard";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseBoardEditor databaseBoardEditor;

		public DeleteBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseBoardEditor databaseBoardEditor) : base(databaseAuthorizer) {
			this.databaseBoardEditor = databaseBoardEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseBoardEditor.Delete(parameters[HttpParameters.BoardId].ToGuid());
		}
	}
}