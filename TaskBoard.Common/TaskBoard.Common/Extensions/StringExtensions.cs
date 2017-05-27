using System;
using TaskBoard.Common.Enums;

namespace TaskBoard.Common.Extensions {
	// ReSharper disable UnusedMember.Global

	public static class StringExtensions {
		public static Guid ToGuid(this string line) {
			return Guid.Parse(line);
		}

		public static DateTime ToDateTime(this string line) {
			return DateTime.Parse(line);
		}

		public static State ToState(this string line) {
			return (State)Enum.Parse(typeof(State), line);
		}

		public static Priority ToPriority(this string line) {
			return (Priority)Enum.Parse(typeof(Priority), line);
		}
	}
}