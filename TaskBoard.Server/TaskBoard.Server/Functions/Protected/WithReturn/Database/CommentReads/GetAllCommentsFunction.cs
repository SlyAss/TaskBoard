using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.CommentReads {
	// ReSharper disable UnusedMember.Global

	public class GetAllCommentsFunction : HttpProtectedFunctionWithReturn<Comment[]> {
		public override string NameOfCalledMethod => "GetAllComments";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseCommentReader databaseCommentReader;

		public GetAllCommentsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentReader databaseCommentReader) : base(databaseAuthorizer) {
			this.databaseCommentReader = databaseCommentReader;
		}

		protected override Comment[] Run(NameValues parameters, byte[] requestBody) {
			return databaseCommentReader.GetAll();
		}
	}
}