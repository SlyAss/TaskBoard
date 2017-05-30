using TaskBoard.Common;

namespace TaskBoard.Client.UI {
	public class ClientUiConfiguration : ConfigurationFile<ClientUiConfiguration> {
		protected override string ConfigurationFileName => "TaskBoard.Client.UI.Configuration.xml";

		public string ServerAddress { get; set; }
		public int TimeoutMs { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public bool SaveLoginAndPassword { get; set; }
	}
}