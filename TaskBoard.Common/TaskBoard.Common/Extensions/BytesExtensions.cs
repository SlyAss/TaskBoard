using TaskBoard.Common.Serializers;

namespace TaskBoard.Common.Extensions {
	// ReSharper disable UnusedMember.Global

	public static class BytesExtensions {
		public static TKey FromJson<TKey>(this byte[] bytes) {
			return JsonSerializer.Deserializing<TKey>(bytes);
		}

		public static TKey FromXml<TKey>(this byte[] bytes) {
			return XmlSerializer.Deserializing<TKey>(bytes);
		}
	}
}