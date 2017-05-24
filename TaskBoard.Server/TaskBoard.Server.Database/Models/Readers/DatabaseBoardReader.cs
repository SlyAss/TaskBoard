using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Extensions;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseBoardReader : DatabaseReader, IDatabaseBoardReader {
		public DatabaseBoardReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public BoardProxy[] GetAll() {
			return ModelDatabase.Boards.ToProxies();
		}

		public BoardProxy[] GetWithUsingFilters(string name) {
			IQueryable<Board> boards = ModelDatabase.Boards;
			UseFilter(name != null, ref boards, board => board.Name.Contains(name));

			return boards.ToProxies();
		}
	}
}