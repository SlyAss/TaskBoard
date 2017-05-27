using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.ColumnReads {
	public class GetColumnsWithUsingFiltersFunction : HttpProtectedFunctionWithReturn<Column[]> {
		public override string NameOfCalledMethod => "GetColumnsWithUsingFilters";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseColumnReader databaseColumnReader;

		public GetColumnsWithUsingFiltersFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnReader databaseColumnReader) : base(databaseAuthorizer) {
			this.databaseColumnReader = databaseColumnReader;
		}

		protected override Column[] Run(NameValues parameters, byte[] requestBody) {
			var header = parameters.GetValueOrNull(HttpParameters.ColumnHeader);
			var boardId = parameters.GetValueOrNull(HttpParameters.ColumnBoardId)?.ToGuid();

			return databaseColumnReader.GetWithUsingFilters(header, boardId);
		}
	}
}