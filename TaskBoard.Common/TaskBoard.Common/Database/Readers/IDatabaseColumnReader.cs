using System;
using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseColumnReader : IDatabaseReader<Column> {
		Column[] GetFromBoard(Guid boardId);
		Column[] GetWithUsingFilters(string header, string brush, Guid? boardId);
	}
}