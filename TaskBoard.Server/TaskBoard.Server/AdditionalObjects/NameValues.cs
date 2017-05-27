using System;
using System.Collections.Generic;

namespace TaskBoard.Server.AdditionalObjects {
	public class NameValues {
		private readonly Dictionary<string, string> nameValues;
		public string this[string key] {
			get {
				if (nameValues.ContainsKey(key))
					return nameValues[key];
				throw new ArgumentException($"Отсутствует ключ {key}");
			}
		}

		public NameValues() : this(new Dictionary<string, string>()) {
		}
		public NameValues(Dictionary<string, string> nameValues) {
			this.nameValues = nameValues;
		}

		public string GetOrThrow(string key, string excetionMessage) {
			if (nameValues.ContainsKey(key))
				return nameValues[key];
			throw new ArgumentException($"Отсутствует ключ {key}; {excetionMessage}");
		}
	}
}