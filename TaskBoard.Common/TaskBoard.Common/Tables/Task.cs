using System;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Common.Tables {
	// ReSharper disable ClassNeverInstantiated.Global
	// ReSharper disable UnusedMember.Global

	public class Task {
		[HeaderColumn("Id задачи")]
		public Guid TaskId { get; set; }

		[HeaderColumn("Заголовок")]
		public string Header { get; set; }

		[HeaderColumn("Описание")]
		public string Description { get; set; }

		[HeaderColumn("Ветка")]
		public string Branch { get; set; }

		[HeaderColumn("Состояние")]
		public State State { get; set; }

		[HeaderColumn("Приоритет")]
		public Priority Priority { get; set; }

		[HeaderColumn("Дата создания")]
		public DateTime CreateDateTime { get; set; }

		[HeaderColumn("Id разработчика")]
		public Guid? DeveloperId { get; set; }

		[HeaderColumn("Id ревьювера")]
		public Guid? ReviewerId { get; set; }

		[HeaderColumn("Id столбца")]
		public Guid? ColumnId { get; set; }

		[HeaderColumn("Id доски")]
		public Guid BoardId { get; set; }
	}
}