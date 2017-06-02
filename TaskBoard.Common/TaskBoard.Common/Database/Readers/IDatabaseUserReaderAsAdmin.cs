using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseUserReaderAsAdmin : IDatabaseReader<User> {
		User[] GetWithUsingFilters(string login);
	}
}