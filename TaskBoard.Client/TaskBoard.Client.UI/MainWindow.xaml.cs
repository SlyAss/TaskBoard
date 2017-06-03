using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskBoard.Client.UI.Windows;

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

			ThisBoardControl.SetHttpClientProvider(httpClientProvider);
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
					clientUiConfiguration.Login = httpClientProvider.Login;
					clientUiConfiguration.Password = httpClientProvider.Password;
					clientUiConfiguration.WriteConfiguration();
					ComboBoxBoards.SelectedItem = string.Empty;
					ThisBoardControl.Clear();
					CreateMenuAuthorization();
				};

				MenuAuthorization.ItemsSource = new[] {
					new MenuItem { Header = httpClientProvider.Login, ItemsSource = new[] { menuItemSignOut }, VerticalContentAlignment = VerticalAlignment.Center, Height = 26 }
				};
			}
			else {
				var menuItemSignIn = new MenuItem { Header = "Войти", VerticalContentAlignment = VerticalAlignment.Center, Height = 26 };
				menuItemSignIn.Click += (sender, args) => {
					if (new AuthorizationWindow(httpClientProvider.GetParameretsClient()).ShowDialog() != true)
						return;

					clientUiConfiguration = ClientUiConfiguration.ReadConfiguration();
					CreateMenuAuthorization();
				};

				MenuAuthorization.ItemsSource = new[] { menuItemSignIn };
			}
		}

		private void MenuItemSettings_OnClick(object sender, RoutedEventArgs e) {
			new SettingsWindow(httpClientProvider).ShowDialog();
		}

		private void ComboBoxBoards_OnDropDownOpened(object sender, EventArgs e) {
			var boardNames = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetAll().Select(board => board.Name).ToArray());
			if (boardNames == null)
				return;

			ComboBoxBoards.ItemsSource = new[] { string.Empty }.Concat(boardNames).ToArray();
		}
		private void ComboBoxBoards_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ThisBoardControl.Clear();

			var boardName = (string)ComboBoxBoards.SelectedItem;
			if (string.IsNullOrEmpty(boardName))
				return;

			ThisBoardControl.LoadBoard(boardName);
		}
	}
}