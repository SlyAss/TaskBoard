using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.BoardEdits {
	// ReSharper disable UnusedMember.Global

	public class AddBoardFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "AddBoard";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseBoardEditor databaseBoardEditor;

		public AddBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseBoardEditor databaseBoardEditor) : base(databaseAuthorizer) {
			this.databaseBoardEditor = databaseBoardEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseBoardEditor.Add(requestBody.FromJson<Board>());
		}
	}
}