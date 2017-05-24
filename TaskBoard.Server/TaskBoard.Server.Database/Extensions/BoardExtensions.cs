using System.Collections.Generic;
using System.Linq;
using TaskBoard.Common.Tables.Proxies;
using TaskBoard.Server.Database.Tables;

namespace TaskBoard.Server.Database.Extensions {
	public static class BoardExtensions {
		public static BoardProxy[] ToProxies(this IEnumerable<Board> boards) {
			return boards.Select(ToProxy).ToArray();
		}
		public static BoardProxy ToProxy(this Board board) {
			return new BoardProxy {
				BoardId = board.BoardId,
				Name = board.Name
			};
		}
	}
}