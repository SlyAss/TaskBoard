using System;
using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.ColumnReads {
	public class GetColumnIdsWithUsingFiltersFunction : HttpProtectedFunctionWithReturn<Guid[]> {
		public override string NameOfCalledMethod => "GetColumnIdsWithUsingFilters";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseColumnReader databaseColumnReader;

		public GetColumnIdsWithUsingFiltersFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnReader databaseColumnReader) : base(databaseAuthorizer) {
			this.databaseColumnReader = databaseColumnReader;
		}

		protected override Guid[] Run(NameValues parameters, byte[] requestBody) {
			var header = parameters.GetValueOrNull(HttpParameters.ColumnHeader);
			var brush = parameters.GetValueOrNull(HttpParameters.ColumnBrush);
			var boardId = parameters.GetValueOrNull(HttpParameters.ColumnBoardId)?.ToGuid();

			return databaseColumnReader.GetIdsWithUsingFilters(header, brush, boardId);
		}
	}
}