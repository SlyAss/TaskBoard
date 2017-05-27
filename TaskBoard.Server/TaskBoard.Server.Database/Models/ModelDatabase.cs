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

		public BoardEntity GetBoard(Guid boardId) {
			return Boards.First(board => board.BoardId == boardId);
		}
		public ColumnEntity GetColumn(Guid? columnId) {
			return columnId == null ? null : Columns.First(column => column.ColumnId == columnId);
		}
		public CommentEntity GetComment(Guid commentId) {
			return Comments.First(comment => comment.CommentId == commentId);
		}
		public TaskEntity GetTask(Guid taskId) {
			return Tasks.First(task => task.TaskId == taskId);
		}
		public UserEntity GetUser(Guid? userId) {
			return userId == null ? null : Users.First(user => user.UserId == userId);
		}
	}
}