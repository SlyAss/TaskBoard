using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Client.UI.Windows {
	public partial class SettingsWindow {
		private readonly HttpClientProvider httpClientProvider;
		private readonly ClientUiConfiguration clientConfiguration;

		public SettingsWindow(HttpClientProvider httpClientProvider) {
			InitializeComponent();

			this.httpClientProvider = httpClientProvider;
			clientConfiguration = LoadClientConfiguration();
		}
		private ClientUiConfiguration LoadClientConfiguration() {
			var configuration = ClientUiConfiguration.ReadConfiguration();
			TextBoxServerAddress.Text = configuration.ServerAddress;
			TextBoxRequestTimeoutMs.Text = configuration.TimeoutMs.ToString();

			return configuration;
		}

		private IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxServerAddress))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelServerAddress);

			if (CommonMethods.Check.FieldIsEmpty(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelRequestTimeoutMs);
			else if (CommonMethods.Check.FieldIsNotNumber(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldIsNotNumber(LabelRequestTimeoutMs);
			else if (CommonMethods.Check.FieldNumberIsNotPositive(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldNumberIsNotPositive(LabelRequestTimeoutMs);
		}

		private void ButtonOk_OnClick(object sender, RoutedEventArgs e) {
			var errors = GetErrors().ToArray();
			if (errors.Any()) {
				CommonMethods.ShowMessageBox.Error(string.Join("\n", errors));
				return;
			}

			try {
				httpClientProvider.GetParameretsClient().SetServerAddress(TextBoxServerAddress.Text, TextBoxRequestTimeoutMs.Text.ToInt());

				clientConfiguration.ServerAddress = httpClientProvider.ServerAddress;
				clientConfiguration.TimeoutMs = httpClientProvider.TimeoutMs;
				clientConfiguration.WriteConfiguration();

				DialogResult = true;
				Close();
			}
			catch (Exception exception) {
				CommonMethods.ShowMessageBox.Error(exception.Message);
			}
		}
		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e) {
			DialogResult = false;
			Close();
		}
	}
}