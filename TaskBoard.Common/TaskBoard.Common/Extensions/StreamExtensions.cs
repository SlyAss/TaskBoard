using System.IO;

namespace TaskBoard.Common.Extensions {
	// ReSharper disable UnusedMember.Global

	public static class StreamExtensions {
		public static void WriteAndDispose(this Stream stream, byte[] bytes) {
			using (stream)
				stream.Write(bytes ?? new byte[0], 0, bytes?.Length ?? 0);
		}

		public static byte[] ReadAndDispose(this Stream stream) {
			using (var streamReader = new StreamReader(stream))
				return GlobalConfiguration.Encoding.GetBytes(streamReader.ReadToEnd());
		}
	}
}