using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	// ReSharper disable UnusedMember.Global

	public class DatabaseCommentReader : DatabaseReader, IDatabaseCommentReader {
		public DatabaseCommentReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Comment GetById(Guid id) {
			return ModelDatabase.GetComment(id).ToTable();
		}

		public Guid[] GetAllIds() {
			return ModelDatabase.Comments.Select(comment => comment.CommentId).ToArray();
		}

		public Comment[] GetAll() {
			return ModelDatabase.Comments.ToTables();
		}

		public Guid[] GetIdsFromTask(Guid taskId) {
			return ModelDatabase.Comments.Where(comment => comment.TaskId == taskId).Select(comment => comment.CommentId).ToArray();
		}

		public Comment[] GetFromTask(Guid taskId) {
			return ModelDatabase.Comments.Where(comment => comment.TaskId == taskId).ToTables();
		}

		public Guid[] GetIdsWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			return GetQueryWithUsingFilters(content, beginCreateDateTime, endCreateDateTime, userId, taskId).Select(comment => comment.CommentId).ToArray();
		}

		public Comment[] GetWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			return GetQueryWithUsingFilters(content, beginCreateDateTime, endCreateDateTime, userId, taskId).ToTables();
		}

		private IQueryable<CommentEntity> GetQueryWithUsingFilters(string content, DateTime? beginCreateDateTime, DateTime? endCreateDateTime, Guid? userId, Guid? taskId) {
			IQueryable<CommentEntity> comments = ModelDatabase.Comments;
			UseFilter(content != null, ref comments, comment => comment.Content.Contains(content));
			UseFilter(beginCreateDateTime != null, ref comments, comment => comment.CreateDateTime >= beginCreateDateTime);
			UseFilter(endCreateDateTime != null, ref comments, comment => comment.CreateDateTime <= endCreateDateTime);
			UseFilter(userId != null, ref comments, comment => comment.UserId == userId);
			UseFilter(taskId != null, ref comments, comment => comment.TaskId == taskId);

			return comments;
		}
	}
}