using System.Collections.Generic;
using System.Windows;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows.Tables {
	public partial class BoardWindow : IWindowWithChecking<Board> {
		public Board Table { get; private set; }

		public BoardWindow(Board board, bool isReadOnly) {
			InitializeComponent();

			LoadWindowData(board, isReadOnly);
		}
		private void LoadWindowData(Board board, bool isReadOnly) {
			CommonMethods.Set.ReadOnly(LabelBoardName, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxBoardName, isReadOnly);
			if (board == null)
				return;

			TextBoxBoardName.Text = board.Name;
		}

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxBoardName))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelBoardName);
		}

		public void ActionBeforeTrueDialogResultClose() {
			Table = new Board {
				Name = TextBoxBoardName.Text
			};
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