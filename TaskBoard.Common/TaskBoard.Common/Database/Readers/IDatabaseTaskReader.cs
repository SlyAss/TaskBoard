using System;
using TaskBoard.Common.Tables.Enums;
using TaskBoard.Common.Tables.Proxies;

namespace TaskBoard.Common.Database.Readers {
	public interface IDatabaseTaskReader : IDatabaseReader<TaskProxy> {
		TaskProxy[] GetAllFromBoard(Guid boardId);
		TaskProxy[] GetWithUsingFilters(string header, string description, string branch, State? state, Priority? priority, Guid? developerId, Guid? reviewerId, Guid? columnId, Guid? boardId);
	}
}