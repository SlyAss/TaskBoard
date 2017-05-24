using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Server.Database.Tables {
	public class User {
		[Key]
		public Guid UserId { get; set; }

		[Required, MaxLength(64), Index("IX_UserUniques", 1, IsUnique = true)]
		public string Login { get; set; }

		[Required, MaxLength(64), Index("IX_UserUniques", 2, IsUnique = true)]
		public string Password { get; set; }

		[Required]
		public int AccessType { get; set; }
	}
}