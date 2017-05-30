using System.Windows.Controls;
using TaskBoard.Client.UI.AdditionalWindows;

namespace TaskBoard.Client.UI {
	public partial class MainWindow {
		private readonly HttpClientProvider httpClientProvider;
		private ClientUiConfiguration clientUiConfiguration;

		public MainWindow() {
			InitializeComponent();

			httpClientProvider = new HttpClientProvider();
			clientUiConfiguration = ClientUiConfiguration.ReadConfiguration();
			LoadHttpClientProviderSettings(clientUiConfiguration);
			CreateMenuAuthorization();
		}
		private void LoadHttpClientProviderSettings(ClientUiConfiguration configuration) {
			CommonMethods.SafeRunMethod.WithoutReturn(() => {
				httpClientProvider.GetParameretsClient().SetServerAddress(configuration.ServerAddress, configuration.TimeoutMs);
				httpClientProvider.GetParameretsClient().SignIn(configuration.Login, configuration.Password);
			}, false);

			RewriteClientUiConfiguration(configuration);
		}
		private void RewriteClientUiConfiguration(ClientUiConfiguration configuration) {
			configuration.ServerAddress = httpClientProvider.ServerAddress;
			configuration.TimeoutMs = httpClientProvider.TimeoutMs;
			configuration.Login = httpClientProvider.Login;
			configuration.Password = httpClientProvider.Password;

			configuration.WriteConfiguration();
		}

		private void CreateMenuAuthorization() {
			if (httpClientProvider.IsAuthorize) {
				var menuItemSignOut = new MenuItem { Header = "Выйти" };
				menuItemSignOut.Click += (sender, args) => {
					httpClientProvider.GetParameretsClient().SingOut();
					CreateMenuAuthorization();
				};

				MenuAuthorization.ItemsSource = new[] { new MenuItem { Header = httpClientProvider.Login, ItemsSource = new[] { menuItemSignOut } } };
			}
			else {
				var menuItemSignIn = new MenuItem { Header = "Войти" };
				menuItemSignIn.Click += (sender, args) => {
					if (new AuthorizationWindow(httpClientProvider.GetParameretsClient()).ShowDialog() != true)
						return;

					clientUiConfiguration = ClientUiConfiguration.ReadConfiguration();
					CreateMenuAuthorization();
				};

				MenuAuthorization.ItemsSource = new[] { menuItemSignIn };
			}
		}
	}
}