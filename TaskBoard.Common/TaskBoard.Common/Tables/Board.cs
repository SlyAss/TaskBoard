using System;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables {
	// ReSharper disable ClassNeverInstantiated.Global
	// ReSharper disable UnusedMember.Global

	public class Board {
		[HeaderColumn("Id доски")]
		public Guid BoardId { get; set; }

		[HeaderColumn("Название")]
		public string Name { get; set; }
	}
}