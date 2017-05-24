using TaskBoard.Common;

namespace TaskBoard.Server {
	public class ServerConfiguration : ConfigurationFile<ServerConfiguration> {
		protected override string ConfigurationFileName => "TaskBoard.Server.Configuration.xml";

		public string ServerAddress { get; set; }
	}
}