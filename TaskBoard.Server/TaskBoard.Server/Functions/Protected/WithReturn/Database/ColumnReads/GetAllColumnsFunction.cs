using TaskBoard.Common.Database;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.Protected.WithReturn.Database.ColumnReads {
	// ReSharper disable UnusedMember.Global

	public class GetAllColumnsFunction : HttpProtectedFunctionWithReturn<Column[]> {
		public override string NameOfCalledMethod => "GetAllColumns";
		protected override AccessType RequiredAccessType => AccessType.UserRead;
		private readonly IDatabaseColumnReader databaseColumnReader;

		public GetAllColumnsFunction(IDatabaseAuthorizer databaseAuthorizer, IDatabaseColumnReader databaseColumnReader) : base(databaseAuthorizer) {
			this.databaseColumnReader = databaseColumnReader;
		}

		protected override Column[] Run(NameValues parameters, byte[] requestBody) {
			return databaseColumnReader.GetAll();
		}
	}
}