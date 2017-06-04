using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Client.UI.Windows {
	public partial class SettingsWindow : IWindowWithChecking {
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

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxServerAddress))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelServerAddress);

			if (CommonMethods.Check.FieldIsEmpty(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelRequestTimeoutMs);
			else if (CommonMethods.Check.FieldIsNotNumber(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldIsNotNumber(LabelRequestTimeoutMs);
			else if (CommonMethods.Check.FieldNumberIsNotPositive(TextBoxRequestTimeoutMs))
				yield return CommonMethods.GenerateMessage.FieldNumberIsNotPositive(LabelRequestTimeoutMs);
		}

		public void ActionBeforeTrueDialogResultClose() {
			httpClientProvider.GetParameretsClient().SetServerAddress(TextBoxServerAddress.Text, TextBoxRequestTimeoutMs.Text.ToInt());

			clientConfiguration.ServerAddress = httpClientProvider.ServerAddress;
			clientConfiguration.TimeoutMs = httpClientProvider.TimeoutMs;
			clientConfiguration.WriteConfiguration();
		}
		public void ActionBeforeFalseDialogResultClose() {
		}

		private void ButtonOk_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.TrueDialogResult(this);
		}
		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.FalseDialogResult(this);
		}
	}
}