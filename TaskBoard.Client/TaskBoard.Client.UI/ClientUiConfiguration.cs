using TaskBoard.Common;

namespace TaskBoard.Client.UI {
	public class ClientUiConfiguration : ConfigurationFile<ClientUiConfiguration> {
		protected override string ConfigurationFileName => "TaskBoard.Client.UI.Configuration.xml";

		public string ServerAddress { get; set; }
		public int TimeoutMs { get; set; }
		public string UserLogin { get; set; }
		public string UserPassword { get; set; }
		public bool SaveLoginAndPassword { get; set; }
	}
}