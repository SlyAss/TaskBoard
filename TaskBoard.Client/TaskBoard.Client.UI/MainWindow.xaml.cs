using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TaskBoard.Client.UI.AdditionalControls;
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

			var stackPanel = new StackPanel {
				Orientation = Orientation.Horizontal,
				Children = {
					new StackPanel {
						Children = {
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.Firebrick) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.Firebrick) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top }
						}
					},
					new Rectangle { Width = 1, Fill = Brushes.DarkGray },
					new StackPanel {
						Children = {
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.Yellow) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.Yellow) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top }
						}
					},
					new Rectangle { Width = 1, Fill = Brushes.DarkGray },
					new StackPanel {
						Children = {
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.DarkGreen) {HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
						}
					},
					new Rectangle { Width = 1, Fill = Brushes.DarkGray },
					new StackPanel {
						Children = {
							new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First(), Brushes.Blue) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
						}
					}
				}
			};

			//var taskControl = new TaskControl(httpClientProvider.GetDatabaseTaskReader().GetAll().First()) { Height = 200, Width = 200, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
			Grid.SetColumn(stackPanel, 1);
			Grid1.Children.Add(stackPanel);
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