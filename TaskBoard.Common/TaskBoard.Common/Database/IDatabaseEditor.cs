using System;

namespace TaskBoard.Common.Database {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseEditor<in TTable> {
		void Add(TTable table);
		void Edit(Guid oldTableId, TTable newTable);
		void Delete(Guid tableId);
	}
}