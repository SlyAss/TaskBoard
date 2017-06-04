using System.Collections.Generic;
using System.Windows;
using TaskBoard.Client.UI.Extensions;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows.Tables {
	public partial class UserWindow : IWindowWithChecking<User> {
		public User Table { get; private set; }

		public UserWindow(User user, bool isReadOnly) {
			InitializeComponent();

			LoadWindowData(user, isReadOnly);
		}
		private void LoadWindowData(User user, bool isReadOnly) {
			CommonMethods.Set.ReadOnly(TextBoxUserLogin, isReadOnly);
			CommonMethods.Set.ReadOnly(PasswordBoxUserPassword, isReadOnly);
			CommonMethods.Set.ReadOnly(CheckBoxUserRead, isReadOnly);
			CommonMethods.Set.ReadOnly(CheckBoxUserWrite, isReadOnly);
			CommonMethods.Set.ReadOnly(CheckBoxAdminRead, isReadOnly);
			CommonMethods.Set.ReadOnly(CheckBoxAdminWrite, isReadOnly);
			if (user == null)
				return;

			TextBoxUserLogin.Text = user.Login;
			PasswordBoxUserPassword.Password = user.Password;
			CheckBoxUserRead.IsChecked = user.IsHaveAccess(AccessType.UserRead);
			CheckBoxUserWrite.IsChecked = user.IsHaveAccess(AccessType.UserWrite);
			CheckBoxAdminRead.IsChecked = user.IsHaveAccess(AccessType.AdminRead);
			CheckBoxAdminWrite.IsChecked = user.IsHaveAccess(AccessType.AdminWrite);
		}

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxUserLogin))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelUserLogin);

			if (CommonMethods.Check.FieldIsEmpty(PasswordBoxUserPassword))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelUserPassword);
		}

		public void ActionBeforeTrueDialogResultClose() {
			Table = new User {
				Login = TextBoxUserLogin.Text,
				Password = PasswordBoxUserPassword.Password,
				AccessType = 0
			};

			if (CheckBoxUserRead.IsChecked == true)
				Table.AddAccess(AccessType.UserRead);
			if (CheckBoxUserWrite.IsChecked == true)
				Table.AddAccess(AccessType.UserWrite);
			if (CheckBoxAdminRead.IsChecked == true)
				Table.AddAccess(AccessType.AdminRead);
			if (CheckBoxAdminWrite.IsChecked == true)
				Table.AddAccess(AccessType.AdminWrite);
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