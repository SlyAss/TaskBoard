using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Client.UI {
	public static class CommonMethods {
		public static class Check {
			public static bool FieldIsEmpty(TextBox textBox) {
				return string.IsNullOrEmpty(textBox.Text);
			}

			public static bool FieldIsEmpty(PasswordBox passwordBox) {
				return string.IsNullOrEmpty(passwordBox.Password);
			}

			public static bool FieldIsNotNumber(TextBox textBox) {
				return !int.TryParse(textBox.Text, out int _);
			}

			public static bool FieldNumberIsNotPositive(TextBox textBox) {
				return textBox.Text.ToInt() <= 0;
			}
		}

		public static class GenerateMessage {
			public static string FieldIsEmpty(Label label) {
				return $"Необходимо заполнить поле '{(string)label.Content}'";
			}

			public static string FieldIsNotNumber(Label label) {
				return $"Поле '{(string)label.Content}' должно быть числом";
			}

			public static string FieldNumberIsNotPositive(Label label) {
				return $"Поле '{(string)label.Content}' должно быть положительным числом";
			}
		}

		public static class ShowMessageBox {
			public static void Information(string successfulMessage) {
				MessageBox.Show(successfulMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
			}

			public static void Error(string exceptionMessage) {
				MessageBox.Show(exceptionMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public static class CloseWindow {
			public static void TrueDialogResult<TWindow>(TWindow window) where TWindow : Window, IWindowWithChecking {
				var errors = window.GetErrors().ToArray();
				if (errors.Any()) {
					ShowMessageBox.Error(string.Join("\n", errors));
					return;
				}

				SafeRunMethod.WithoutReturn(() => {
					window.ActionBeforeTrueDialogResultClose();
					window.DialogResult = true;
					window.Close();
				}, true);
			}

			public static void FalseDialogResult<TWindow>(TWindow window) where TWindow : Window, IWindowWithChecking {
				window.ActionBeforeFalseDialogResultClose();
				window.DialogResult = false;
				window.Close();
			}
		}

		public static class SafeRunMethod {
			public static TKey WithReturn<TKey>(Func<TKey> action, bool showErrors = true, string successfulMessage = null) {
				var result = default(TKey);
				RunMethod(() => result = action(), showErrors, successfulMessage);
				return result;
			}

			public static void WithoutReturn(Action action, bool showErrors = true, string successfulMessage = null) {
				RunMethod(action, showErrors, successfulMessage);
			}

			private static void RunMethod(Action action, bool showErrors, string successfulMessage) {
				try {
					action();

					if (!string.IsNullOrEmpty(successfulMessage))
						ShowMessageBox.Information(successfulMessage);
				}
				catch (Exception exception) {
					if (showErrors)
						ShowMessageBox.Error(exception.Message);
				}
			}
		}
	}
}