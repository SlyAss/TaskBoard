using System;
using TaskBoard.Common.Tables;

namespace TaskBoard.Common.Database.Readers {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseCommentReader : IDatabaseReader<Comment> {
		Guid[] GetIdsFromTask(Guid taskId);
		Comment[] GetFromTask(Guid taskId);
		Guid[] GetIdsWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId);
		Comment[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId);
	}
}