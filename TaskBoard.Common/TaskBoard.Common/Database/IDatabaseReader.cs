using System;

namespace TaskBoard.Common.Database {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseReader<out TTable> {
		TTable GetById(Guid id);
		Guid[] GetAllIds();
		TTable[] GetAll();
	}
}