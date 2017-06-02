using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.CommentReads {
	public class GetAllCommentIdsFunction : HttpProtectedFunctionWithReturn<Guid[]> {
		public override string NameOfCalledMethod => "GetAllCommentIds";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseCommentReader databaseCommentReader;

		public GetAllCommentIdsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentReader databaseCommentReader) : base(databaseAuthorizer) {
			this.databaseCommentReader = databaseCommentReader;
		}

		protected override Guid[] Run(NameValues parameters, byte[] requestBody) {
			return databaseCommentReader.GetAllIds();
		}
	}
}