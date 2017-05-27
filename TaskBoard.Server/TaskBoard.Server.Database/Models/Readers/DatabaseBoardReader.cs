using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	public class DatabaseBoardReader : DatabaseReader, IDatabaseBoardReader {
		public DatabaseBoardReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Board[] GetAll() {
			return ModelDatabase.Boards.ToTables();
		}

		public Board[] GetWithUsingFilters(string name) {
			IQueryable<BoardEntity> boards = ModelDatabase.Boards;
			UseFilter(name != null, ref boards, board => board.Name.Contains(name));

			return boards.ToTables();
		}
	}
}