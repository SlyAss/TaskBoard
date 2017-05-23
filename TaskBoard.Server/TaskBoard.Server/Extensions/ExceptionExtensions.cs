using System;
using System.Collections.Generic;

namespace TaskBoard.Server.Extensions {
	public static class ExceptionExtensions {
		public static IEnumerable<string> GetErrorMessages(this Exception exception) {
			while (exception != null) {
				if (!string.IsNullOrEmpty(exception.Message))
					yield return exception.Message;
				exception = exception.InnerException;
			}
		}
	}
}