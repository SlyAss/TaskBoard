namespace TaskBoard.Common.Database {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseReader<out TProxy> {
		TProxy[] GetAll();
	}
}