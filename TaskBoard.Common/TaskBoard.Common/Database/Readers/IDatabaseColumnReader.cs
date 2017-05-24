using System;
using TaskBoard.Common.Tables.Proxies;

namespace TaskBoard.Common.Database.Readers {
	public interface IDatabaseColumnReader : IDatabaseReader<ColumnProxy> {
		ColumnProxy[] GetWithUsingFilters(string header, Guid boardId);
	}
}