using System;
using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseColumnReader : IDatabaseReader<Column> {
		Guid[] GetIdsFromBoard(Guid boardId);
		Column[] GetFromBoard(Guid boardId);
		Guid[] GetIdsWithUsingFilters(string header, string brush, Guid? boardId);
		Column[] GetWithUsingFilters(string header, string brush, Guid? boardId);
	}
}