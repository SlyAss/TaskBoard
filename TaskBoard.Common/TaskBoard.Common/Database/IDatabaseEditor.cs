using System;

namespace TaskBoard.Common.Database {
	// ReSharper disable UnusedMember.Global

	public interface IDatabaseEditor<in TProxy> {
		void Add(TProxy proxy);
		void Edit(Guid oldProxyId, TProxy newProxy);
		void Delete(Guid proxyId);
	}
}