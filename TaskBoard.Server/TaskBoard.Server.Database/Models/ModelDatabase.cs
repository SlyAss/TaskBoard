using System.Data.Entity;
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
	}
}