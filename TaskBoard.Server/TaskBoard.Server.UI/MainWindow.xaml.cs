using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TaskBoard.Common.Extensions;
using TaskBoard.Common.Http;

namespace TaskBoard.Server.UI {
	public partial class MainWindow {
		private readonly ServerUiConfiguration serverUiConfiguration;
		private readonly ServerConfiguration serverConfiguration;

		public MainWindow() {
			InitializeComponent();
			serverUiConfiguration = LoadServerUiConfiguration();
			serverConfiguration = LoadServerConfiguration();
		}
		private ServerConfiguration LoadServerConfiguration() {
			var configuration = ServerConfiguration.ReadConfiguration();
			TextBoxServerAddress.Text = configuration.ServerAddress;

			return configuration;
		}
		private ServerUiConfiguration LoadServerUiConfiguration() {
			var configuration = ServerUiConfiguration.ReadConfiguration();
			TextBoxUserLogin.Text = configuration.UserLogin;
			PasswordBoxUserPassword.Password = configuration.UserPassword;
			CheckBoxSaveLoginAndPassword.IsChecked = configuration.SaveLoginAndPassword;

			return configuration;
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
			var saveLoginAndPassword = CheckBoxSaveLoginAndPassword.IsChecked == true;

			serverUiConfiguration.UserLogin = saveLoginAndPassword ? TextBoxUserLogin.Text : string.Empty;
			serverUiConfiguration.UserPassword = saveLoginAndPassword ? PasswordBoxUserPassword.Password : string.Empty;
			serverUiConfiguration.SaveLoginAndPassword = saveLoginAndPassword;

			serverUiConfiguration.WriteConfiguration();
		}

		private async void ButtonRunServer_OnClick(object sender, RoutedEventArgs e) {
			SetEnabledControls(true);

			try {
				serverConfiguration.ServerAddress = new UriBuilder(TextBoxServerAddress.Text).ToString();
				serverConfiguration.WriteConfiguration();
				await Task.Run(() => Program.RunServer());
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			SetEnabledControls(false);
		}
		private void ButtonStopServer_OnClick(object sender, RoutedEventArgs e) {
			try {
				var webRequest = (HttpWebRequest)WebRequest.Create($"{serverConfiguration.ServerAddress}Stop?{GetParametersForStop()}");
				webRequest.Method = "GET";
				webRequest.Timeout = 5000;
				webRequest.GetResponse();
			}
			catch (WebException exception) {
				var exceptionMessage = string.Join("\n",
					new[] { exception.Message, exception.Response?.GetResponseStream().ReadAndDispose().FromJson<string>() }
						.Where(message => !string.IsNullOrEmpty(message)));

				MessageBox.Show(exceptionMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private string GetParametersForStop() {
			return $"{HttpParameters.Login}={TextBoxUserLogin.Text}&{HttpParameters.Password}={PasswordBoxUserPassword.Password}";
		}

		private void SetEnabledControls(bool serverIsRun) {
			LabelServerAddress.Foreground = serverIsRun ? Brushes.Silver : Brushes.DodgerBlue;
			TextBoxServerAddress.IsEnabled = !serverIsRun;
			ButtonRunServer.IsEnabled = !serverIsRun;
			ButtonStopServer.IsEnabled = serverIsRun;
		}
	}
}