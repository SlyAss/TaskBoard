using TaskBoard.Common;

namespace TaskBoard.Server.UI {
	public class ServerUiConfiguration : ConfigurationFile<ServerUiConfiguration> {
		protected override string ConfigurationFileName => "TaskBoard.Server.UI.Configuration.xml";

		public string UserLogin { get; set; }
		public string UserPassword { get; set; }
		public bool SaveLoginAndPassword { get; set; }
	}
}