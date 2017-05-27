using System;

namespace TaskBoard.Common.Extensions {
	// ReSharper disable UnusedMember.Global

	public static class StringExtensions {
		public static Guid ToGuid(this string line) {
			return Guid.Parse(line);
		}
	}
}