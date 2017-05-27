using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.CommentReads {
	public class GetCommentsWithUsingFiltersFunction : HttpProtectedFunctionWithReturn<Comment[]> {
		public override string NameOfCalledMethod => "GetCommentsWithUsingFilters";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseCommentReader databaseCommentReader;

		public GetCommentsWithUsingFiltersFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentReader databaseCommentReader) : base(databaseAuthorizer) {
			this.databaseCommentReader = databaseCommentReader;
		}

		protected override Comment[] Run(NameValues parameters, byte[] requestBody) {
			var content = parameters.GetValueOrNull(HttpParameters.CommentContent);
			var beginCreateDateTime = parameters.GetValueOrNull(HttpParameters.CommentBeginCreateDateTime)?.ToDateTime();
			var endCreateDateTime = parameters.GetValueOrNull(HttpParameters.CommentEndCreateDateTime)?.ToDateTime();
			var userId = parameters.GetValueOrNull(HttpParameters.CommentUserId)?.ToGuid();
			var taskId = parameters.GetValueOrNull(HttpParameters.CommentTaskId)?.ToGuid();

			return databaseCommentReader.GetWithUsingFilters(content, beginCreateDateTime, endCreateDateTime, userId, taskId);
		}
	}
}