using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithoutReturn.Database.CommentEdits {
	public class AddCommentFunction : HttpProtectedFunctionWithoutReturn {
		public override string NameOfCalledMethod => "AddComment";
		protected override AccessType RequiredAccessType => AccessType.UserWrite;
		private readonly IDatabaseCommentEditor databaseCommentEditor;

		public AddCommentFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentEditor databaseCommentEditor) : base(databaseAuthorizer) {
			this.databaseCommentEditor = databaseCommentEditor;
		}

		protected override void Run(NameValues parameters, byte[] requestBody) {
			databaseCommentEditor.Add(requestBody.FromJson<Comment>());
		}
	}
}