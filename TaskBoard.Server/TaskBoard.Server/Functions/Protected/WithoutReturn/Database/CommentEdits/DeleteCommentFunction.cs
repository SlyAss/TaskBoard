using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.CommentEdits {
	// ReSharper disable UnusedMember.Global

	public class DeleteCommentFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "DeleteComment";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseCommentEditor databaseCommentEditor;

		public DeleteCommentFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentEditor databaseCommentEditor) : base(databaseAuthorizer) {
			this.databaseCommentEditor = databaseCommentEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseCommentEditor.Delete(parameters[HttpParameters.CommentId].ToGuid());
		}
	}
}