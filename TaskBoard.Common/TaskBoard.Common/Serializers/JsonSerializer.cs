using Newtonsoft.Json;

namespace TaskBoard.Common.Serializers {
	internal static class JsonSerializer {
		public static byte[] Serializing(object obj) {
			return GlobalConfiguration.Encoding.GetBytes(JsonConvert.SerializeObject(obj));
		}

		public static TKey Deserializing<TKey>(byte[] bytes) {
			return JsonConvert.DeserializeObject<TKey>(GlobalConfiguration.Encoding.GetString(bytes));
		}
	}
}