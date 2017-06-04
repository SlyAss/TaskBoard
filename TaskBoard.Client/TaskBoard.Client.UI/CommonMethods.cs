using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskBoard.Common.Database;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Client.UI {
	// ReSharper disable MemberCanBePrivate.Global

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

			public static bool FieldIsNotBrush(TextBox textBox) {
				return !Regex.IsMatch(textBox.Text, @"#[0-9A-F]{8}");
			}

			public static bool FieldIsEmpty(ComboBox comboBox) {
				return string.IsNullOrEmpty((string)comboBox.SelectedItem);
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

			public static string FieldIsNotBrush(Label label) {
				return $"Поле '{(string)label.Content}' должно содержать код цвета";
			}
		}

		public static class Set {
			public static void ReadOnly(Label label, bool isReadOnly) {
				label.Foreground = isReadOnly ? Brushes.Silver : Brushes.DodgerBlue;
			}

			public static void ReadOnly(TextBox textBox, bool isReadOnly) {
				textBox.IsReadOnly = isReadOnly;
			}

			public static void ReadOnly(PasswordBox passwordBox, bool isReadOnly) {
				passwordBox.IsEnabled = !isReadOnly;
			}

			public static void ReadOnly(ComboBox comboBox, bool isReadOnly) {
				comboBox.IsReadOnly = isReadOnly;
			}

			public static void ReadOnly(CheckBox checkBox, bool isReadOnly) {
				checkBox.IsEnabled = !isReadOnly;
			}

			public static void ReadOnly(DatePicker datePicker, bool isReadOnly) {
				datePicker.IsEnabled = !isReadOnly;
			}
		}

		public static class WorkWithTables {
			public static void View<TTable>(TTable selectedItem, Func<TTable, bool, Window> getWindow) {
				if (selectedItem == null)
					return;

				getWindow(selectedItem, true).ShowDialog();
			}

			public static void Add<TTable, TWindow>(Func<TTable, bool, TWindow> getWindow, IDatabaseEditor<TTable> databaseEditor, Action actionAfterSuccess = null) where TWindow : Window, IWindowWithChecking<TTable> {
				var window = getWindow(default(TTable), false);
				if (window.ShowDialog() != true)
					return;

				SafeRunMethod.WithoutReturn(() => {
					databaseEditor.Add(window.Table);
					actionAfterSuccess?.Invoke();
				});
			}

			public static void Edit<TTable, TWindow>(TTable selectedItem, Func<TTable, bool, TWindow> getWindow, IDatabaseEditor<TTable> databaseEditor, Func<TTable, Guid> getId, Action actionAfterSuccess = null) where TWindow : Window, IWindowWithChecking<TTable> {
				if (selectedItem == null)
					return;

				var window = getWindow(selectedItem, false);
				if (window.ShowDialog() != true)
					return;

				SafeRunMethod.WithoutReturn(() => {
					databaseEditor.Edit(getId(selectedItem), window.Table);
					actionAfterSuccess?.Invoke();
				});
			}

			public static void Delete<TTable>(TTable selectedItem, IDatabaseEditor<TTable> databaseEditor, Func<TTable, Guid> getId, Action actionAfterSuccess = null) {
				if (selectedItem == null)
					return;

				SafeRunMethod.WithoutReturn(() => {
					databaseEditor.Delete(getId(selectedItem));
					actionAfterSuccess?.Invoke();
				});
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
				});
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