using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.CommentReads {
	// ReSharper disable UnusedMember.Global

	public class GetCommentByIdFunction : HttpProtectedFunctionWithReturn<Comment> {
		public override string NameOfCalledMethod => "GetCommentById";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseCommentReader databaseCommentReader;

		public GetCommentByIdFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseCommentReader databaseCommentReader) : base(databaseAuthorizer) {
			this.databaseCommentReader = databaseCommentReader;
		}

		protected override Comment Run(NameValues parameters, byte[] requestBody) {
			return databaseCommentReader.GetById(parameters[HttpParameters.CommentId].ToGuid());
		}
	}
}