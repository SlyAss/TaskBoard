namespace TaskBoard.Common.Database {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseAuthorizer {
		bool UserIsExist(string login, string password);
		bool AccessIsAllowed(string login, string password, int requestedAccessType);
	}
}