using TaskBoard.Common.Serializers;

namespace TaskBoard.Common.Extensions {
	// ReSharper disable UnusedMember.Global

	public static class ObjectExtensions {
		public static byte[] ToJson(this object obj) {
			return JsonSerializer.Serializing(obj);
		}

		public static byte[] ToXml(this object obj) {
			return XmlSerializer.Serializing(obj);
		}
	}
}