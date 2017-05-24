using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Server.Database.Tables {
	public class Board {
		[Key]
		public Guid BoardId { get; set; }

		[Required, MaxLength(128), Index("IX_BoardUniques", 1, IsUnique = true)]
		public string Name { get; set; }
	}
}