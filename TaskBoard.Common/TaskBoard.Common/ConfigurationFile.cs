using System;
using System.IO;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Common {
	// ReSharper disable UnusedMember.Global

	public abstract class ConfigurationFile<TConfiguration> where TConfiguration : ConfigurationFile<TConfiguration>, new() {
		protected abstract string ConfigurationFileName { get; }
		private string configurationFileDirectory;

		public static TConfiguration ReadConfiguration() {
			var configurationFileName = new TConfiguration().ConfigurationFileName;

			var configurationFileDirectory = Path.GetDirectoryName(Path.GetFullPath(configurationFileName));
			while (!string.IsNullOrEmpty(configurationFileDirectory) && !File.Exists(Path.Combine(configurationFileDirectory, configurationFileName)))
				configurationFileDirectory = Path.GetDirectoryName(configurationFileDirectory);

			if (string.IsNullOrEmpty(configurationFileDirectory))
				return new TConfiguration();

			try {
				var configuration = File.ReadAllBytes(Path.Combine(configurationFileDirectory, configurationFileName)).FromXml<TConfiguration>();
				configuration.configurationFileDirectory = configurationFileDirectory;
				return configuration;
			}
			catch (Exception) {
				return new TConfiguration();
			}
		}
		public void WriteConfiguration() {
			File.WriteAllBytes(string.IsNullOrEmpty(configurationFileDirectory)
					? ConfigurationFileName
					: Path.Combine(configurationFileDirectory, ConfigurationFileName),
				this.ToXml());
		}
	}
}