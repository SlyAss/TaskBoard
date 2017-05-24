using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Models {
	public class ModelDatabase : DbContext {
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Column> Columns { get; set; }
		public DbSet<Board> Boards { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<User> Users { get; set; }

		public ModelDatabase() : base("TaskBoardDatabase") {
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}

		public Board GetBoard(Guid boardId) {
			return Boards.First(board => board.BoardId == boardId);
		}
		public Column GetColumn(Guid? columnId) {
			return columnId == null ? null : Columns.First(column => column.ColumnId == columnId);
		}
		public Comment GetComment(Guid commentId) {
			return Comments.First(comment => comment.CommentId == commentId);
		}
		public Task GetTask(Guid taskId) {
			return Tasks.First(task => task.TaskId == taskId);
		}
		public User GetUser(Guid? userId) {
			return userId == null ? null : Users.First(user => user.UserId == userId);
		}
	}
}