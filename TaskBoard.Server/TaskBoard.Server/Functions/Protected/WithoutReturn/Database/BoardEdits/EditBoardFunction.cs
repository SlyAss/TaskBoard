using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.BoardEdits {
	// ReSharper disable UnusedMember.Global

	public class EditBoardFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "EditBoard";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseBoardEditor databaseBoardEditor;

		public EditBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseBoardEditor databaseBoardEditor) : base(databaseAuthorizer) {
			this.databaseBoardEditor = databaseBoardEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseBoardEditor.Edit(parameters[HttpParameters.BoardId].ToGuid(), requestBody.FromJson<Board>());
		}
	}
}