using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables.Proxies {
	public class BoardProxy {
		[HeaderColumn("Id доски")]
		public Guid BoardId { get; set; }

		[HeaderColumn("Название")]
		public string Name { get; set; }
	}
}