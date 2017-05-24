using System;
using TaskBoard.Common.Tables.Proxies;

namespace TaskBoard.Common.Database.Readers {
	public interface IDatabaseCommentReader : IDatabaseReader<CommentProxy> {
		CommentProxy[] GetAllFromTask(Guid taskId);
		CommentProxy[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId);
	}
}