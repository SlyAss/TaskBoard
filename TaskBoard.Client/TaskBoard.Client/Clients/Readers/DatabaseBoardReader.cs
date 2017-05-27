using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Http;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.Clients.Readers {
	public class DatabaseBoardReader : BaseHttpClient, IDatabaseBoardReader {
		public DatabaseBoardReader(HttpClientParameters httpClientParameters) : base(httpClientParameters) {
		}

		public Board[] GetAll() {
			return SendRequest<Board[]>("GetAllBoards", GetDefaultParameters());
		}

		public Board[] GetWithUsingFilters(string name) {
			var parameters = GetDefaultParameters();
			AddParameterIfNotNullOrEmpty(parameters, HttpParameters.BoardName, name);

			return SendRequest<Board[]>("GetBoardsWithUsingFilters", parameters);
		}
	}
}