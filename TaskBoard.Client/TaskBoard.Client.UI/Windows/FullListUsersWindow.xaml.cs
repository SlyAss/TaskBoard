using System.Windows;
using System.Windows.Input;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows {
	public partial class FullListUsersWindow {
		private readonly HttpClientProvider httpClientProvider;
		private User SelectedUser => (User)DataGridUsers.SelectedItem;

		public FullListUsersWindow(HttpClientProvider httpClientProvider) {
			InitializeComponent();
			this.httpClientProvider = httpClientProvider;

			ReloadDataGridUsersItemsSource();
		}
		private void ReloadDataGridUsersItemsSource() {
			DataGridUsers.ItemsSource = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReaderAsAdmin().GetAll());
		}

		private void ButtonOk_OnClick(object sender, RoutedEventArgs e) {
			Close();
		}

		private void DataGridUsers_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
			CommonMethods.WorkWithTables.View(SelectedUser, (user, isReadOnly) => new UserWindow(user, isReadOnly));
		}
		private void MenuItemAddUsers_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((user, isReadOnly) => new UserWindow(user, isReadOnly), httpClientProvider.GetDatabaseUserEditor(), ReloadDataGridUsersItemsSource);
		}
		private void MenuItemEditUsers_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Edit(SelectedUser, (user, isReadOnly) => new UserWindow(user, isReadOnly), httpClientProvider.GetDatabaseUserEditor(), user => user.UserId, ReloadDataGridUsersItemsSource);
		}
		private void MenuItemDeleteUsers_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(SelectedUser, httpClientProvider.GetDatabaseUserEditor(), user => user.UserId, ReloadDataGridUsersItemsSource);
		}
	}
}