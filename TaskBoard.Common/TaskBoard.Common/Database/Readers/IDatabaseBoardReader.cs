using TaskBoard.Common.Tables.Proxies;

namespace TaskBoard.Common.Database.Readers {
	public interface IDatabaseBoardReader : IDatabaseReader<BoardProxy> {
		BoardProxy[] GetWithUsingFilters(string name);
	}
}