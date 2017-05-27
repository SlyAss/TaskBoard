using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.CommentReads {
	public class GetCommentsFromTaskFunction : HttpProtectedFunctionWithReturn<Comment[]> {
		public override string NameOfCalledMethod => "GetCommentsFromTask";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseCommentReader databaseCommentReader;

		public GetCommentsFromTaskFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentReader databaseCommentReader) : base(databaseAuthorizer) {
			this.databaseCommentReader = databaseCommentReader;
		}

		protected override Comment[] Run(NameValues parameters, byte[] requestBody) {
			return databaseCommentReader.GetFromTask(parameters[HttpParameters.CommentTaskId].ToGuid());
		}
	}
}