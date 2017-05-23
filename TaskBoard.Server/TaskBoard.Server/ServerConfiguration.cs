using TaskBoard.Common;

namespace TaskBoard.Server {
	public class ServerConfiguration : ConfigurationFile<ServerConfiguration> {
		protected override string ConfigurationFileName => "GraduateWork.Server.Configuration.xml";

		public string ServerAddress { get; set; }
	}
}