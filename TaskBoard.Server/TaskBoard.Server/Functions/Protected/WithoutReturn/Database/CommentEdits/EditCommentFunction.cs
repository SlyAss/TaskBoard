using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.CommentEdits {
	// ReSharper disable UnusedMember.Global

	public class EditCommentFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "EditComment";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseCommentEditor databaseCommentEditor;

		public EditCommentFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentEditor databaseCommentEditor) : base(databaseAuthorizer) {
			this.databaseCommentEditor = databaseCommentEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseCommentEditor.Edit(parameters[HttpParameters.CommentId].ToGuid(), requestBody.FromJson<Comment>());
		}
	}
}