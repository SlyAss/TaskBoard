using System.Collections.Generic;
using System.Windows;
using TaskBoard.Client.Clients;

namespace TaskBoard.Client.UI.AdditionalWindows {
	public partial class AuthorizationWindow : IWindowWithChecking {
		private readonly ClientUiConfiguration clientUiConfiguration;
		private readonly ParameretsClient parameretsClient;

		public AuthorizationWindow(ParameretsClient parameretsClient) {
			InitializeComponent();

			this.parameretsClient = parameretsClient;
			clientUiConfiguration = ReadClientUiConfiguration();
		}
		private ClientUiConfiguration ReadClientUiConfiguration() {
			var configuration = ClientUiConfiguration.ReadConfiguration();
			TextBoxLogin.Text = configuration.Login;
			PasswordBoxPassword.Password = configuration.Password;
			CheckBoxSaveLoginAndPassword.IsChecked = configuration.SaveLoginAndPassword;

			return configuration;
		}

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxLogin))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelLogin);

			if (CommonMethods.Check.FieldIsEmpty(PasswordBoxPassword))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelPassword);
		}

		public void ActionBeforeTrueDialogResultClose() {
			parameretsClient.SignIn(TextBoxLogin.Text, PasswordBoxPassword.Password);

			clientUiConfiguration.SaveLoginAndPassword = CheckBoxSaveLoginAndPassword.IsChecked == true;
			clientUiConfiguration.Login = clientUiConfiguration.SaveLoginAndPassword ? TextBoxLogin.Text : null;
			clientUiConfiguration.Password = clientUiConfiguration.SaveLoginAndPassword ? PasswordBoxPassword.Password : null;

			clientUiConfiguration.WriteConfiguration();
		}

		public void ActionBeforeFalseDialogResultClose() {
			if (CheckBoxSaveLoginAndPassword.IsChecked == true)
				return;

			clientUiConfiguration.SaveLoginAndPassword = false;
			clientUiConfiguration.Login = null;
			clientUiConfiguration.Password = null;

			clientUiConfiguration.WriteConfiguration();
		}

		private void ButtonSignIn_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.TrueDialogResult(this);
		}
		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.FalseDialogResult(this);
		}
	}
}