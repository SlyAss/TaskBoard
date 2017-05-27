using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.ColumnReads {
	public class GetColumnsFromBoardFunction : HttpProtectedFunctionWithReturn<Column[]> {
		public override string NameOfCalledMethod => "GetColumnsFromBoard";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseColumnReader databaseColumnReader;

		public GetColumnsFromBoardFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnReader databaseColumnReader) : base(databaseAuthorizer) {
			this.databaseColumnReader = databaseColumnReader;
		}

		protected override Column[] Run(NameValues parameters, byte[] requestBody) {
			return databaseColumnReader.GetFromBoard(parameters[HttpParameters.ColumnBoardId].ToGuid());
		}
	}
}