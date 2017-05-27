using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Extensions {
	public static class BoardExtensions {
		public static Board[] ToTables(this IEnumerable<BoardEntity> boards) {
			return boards.Select(ToTable).ToArray();
		}
		public static Board ToTable(this BoardEntity board) {
			return new Board {
				BoardId = board.BoardId,
				Name = board.Name
			};
		}
	}
}