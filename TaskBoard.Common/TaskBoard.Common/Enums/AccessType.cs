namespace TaskBoard.Common.Enums {
	// ReSharper disable UnusedMember.Global

	public enum AccessType {
		UserRead = 0x01,
		UserWrite = 0x02,
		AdminRead = 0x04,
		AdminWrite = 0x08
	}
}