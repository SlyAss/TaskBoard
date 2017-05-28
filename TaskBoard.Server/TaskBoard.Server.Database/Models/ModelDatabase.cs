using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models {
	// ReSharper disable ClassNeverInstantiated.Global
	// ReSharper disable UnusedAutoPropertyAccessor.Global

	public class ModelDatabase : DbContext {
		public DbSet<CommentEntity> Comments { get; set; }
		public DbSet<ColumnEntity> Columns { get; set; }
		public DbSet<BoardEntity> Boards { get; set; }
		public DbSet<TaskEntity> Tasks { get; set; }
		public DbSet<UserEntity> Users { get; set; }

		public ModelDatabase() : base("TaskBoardDatabase") {
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}

		public override int SaveChanges() {
			try {
				return base.SaveChanges();
			}
			catch (Exception) {
				ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Detached);
				throw;
			}
		}

		public BoardEntity GetBoard(Guid boardId) {
			return Boards
					.FirstOrDefault(b => b.BoardId == boardId)
				?? throw new ArgumentException($"Не найдена доска с id = '{boardId}'");
		}
		public ColumnEntity GetColumn(Guid? columnId) {
			return columnId == null ? null : GetColumn(columnId.Value);
		}
		public ColumnEntity GetColumn(Guid columnId) {
			return Columns
					.Include(c => c.Board)
					.FirstOrDefault(c => c.ColumnId == columnId)
				?? throw new ArgumentException($"Не найден столбец с id = '{columnId}'");
		}
		public CommentEntity GetComment(Guid commentId) {
			return Comments
					.Include(c => c.User)
					.Include(c => c.Task)
					.FirstOrDefault(c => c.CommentId == commentId)
				?? throw new ArgumentException($"Не найден комментарий с id = '{commentId}'");
		}
		public TaskEntity GetTask(Guid taskId) {
			return Tasks
					.Include(t => t.Developer)
					.Include(t => t.Reviewer)
					.Include(t => t.Column)
					.Include(t => t.Board)
					.FirstOrDefault(t => t.TaskId == taskId)
				?? throw new ArgumentException($"Не найдена задача с id = '{taskId}'");
		}
		public UserEntity GetUser(Guid? userId) {
			return userId == null ? null : GetUser(userId.Value);
		}
		public UserEntity GetUser(Guid userId) {
			return Users
					.FirstOrDefault(u => u.UserId == userId)
				?? throw new ArgumentException($"Не найден пользователь с id = '{userId}'");
		}
	}
}