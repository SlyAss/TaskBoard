using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseBoardReader : IDatabaseReader<Board> {
		Board[] GetWithUsingFilters(string name);
	}
}