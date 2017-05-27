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

		public string GetValueOrNull(string key) {
			return nameValues.ContainsKey(key) ? nameValues[key] : null;
		}

		public string GetValueOrThrow(string key, string additionalExceptionMessage) {
			try {
				return this[key];
			}
			catch (ArgumentException exception) {
				throw new ArgumentException($"{exception.Message}. {additionalExceptionMessage}");
			}
		}
	}
}