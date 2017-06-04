using System.Collections.Generic;

namespace TaskBoard.Client.UI {
	public interface IWindowWithChecking<out TTable> : IWindowWithChecking {
		TTable Table { get; }
	}

	public interface IWindowWithChecking {
		IEnumerable<string> GetErrors();
		void ActionBeforeTrueDialogResultClose();
		void ActionBeforeFalseDialogResultClose();
	}
}