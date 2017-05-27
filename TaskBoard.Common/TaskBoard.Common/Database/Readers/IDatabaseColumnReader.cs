using System;
using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseColumnReader : IDatabaseReader<Column> {
		Column[] GetWithUsingFilters(string header, Guid? boardId);
	}
}