using TaskBoard.Common.Tables.Proxies;

namespace TaskBoard.Common.Database.Readers {
	public interface IDatabaseUserReader : IDatabaseReader<UserProxy> {
		UserProxy[] GetWithUsingFilters(string login, string password);
	}
}