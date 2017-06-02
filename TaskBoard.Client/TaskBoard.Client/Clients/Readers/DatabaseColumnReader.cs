using System;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseColumnReader : BaseHttpClient, IDatabaseColumnReader {
		public DatabaseColumnReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public Column GetById(Guid id) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.ColumnId] = id.ToString();

			return SendRequest<Column>("GetColumnById", parameters);
		}

		public Column[] GetAll() {
			return SendRequest<Column[]>("GetAllColumns", GetDefaultParameters());
		}

		public Column[] GetFromBoard(Guid boardId) {
			var parameters = GetDefaultParameters();
			parameters[HttpParameters.ColumnBoardId] = boardId.ToString();

			return SendRequest<Column[]>("GetColumnsFromBoard", parameters);
		}

		public Column[] GetWithUsingFilters(string header, string brush, Guid? boardId) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.ColumnHeader, header);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.ColumnBrush, brush);
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.ColumnBoardId, boardId?.ToString());

			return SendRequest<Column[]>("GetColumnsWithUsingFilters", parameters);
		}
	}
}