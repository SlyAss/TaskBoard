using System;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseTaskReader : IDatabaseReader<Task> {
		Guid[] GetIdsFromBoard(Guid boardId);
		Task[] GetFromBoard(Guid boardId);
		Guid[] GetIdsFromColumn(Guid boardId, Guid columnId);
		Task[] GetFromColumn(Guid boardId, Guid columnId);
		Guid[] GetIdsWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId);
		Task[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId);
	}
}