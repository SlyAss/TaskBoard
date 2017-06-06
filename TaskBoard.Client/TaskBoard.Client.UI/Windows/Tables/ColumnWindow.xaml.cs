using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows.Tables {
	public partial class ColumnWindow : IWindowWithChecking<Column> {
		public Column Table { get; private set; }
		private readonly Dictionary<string, Guid> boardNames;

		public ColumnWindow(Column column, Dictionary<string, Guid> boardNames, bool isReadOnly) {
			InitializeComponent();
			this.boardNames = boardNames;
			LoadWindowData(column, boardNames, isReadOnly);
		}
		private void LoadWindowData(Column column, Dictionary<string, Guid> boardNames, bool isReadOnly) {
			CommonMethods.Set.ReadOnly(LabelColumnHeader, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxColumnHeader, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelColumnBrush, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxColumnBrush, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelColumnBoard, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxColumnBoard, isReadOnly);
			ComboBoxColumnBoard.ItemsSource = boardNames.Keys.ToArray();
			if (column == null)
				return;

			TextBoxColumnHeader.Text = column.Header;
			TextBoxColumnBrush.Text = column.Brush;
			ComboBoxColumnBoard.SelectedItem = boardNames.First(boardName => boardName.Value == column.BoardId).Key;
		}

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxColumnHeader))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelColumnHeader);

			if (CommonMethods.Check.FieldIsEmpty(TextBoxColumnBrush))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelColumnBrush);
			else if (CommonMethods.Check.FieldIsNotBrush(TextBoxColumnBrush))
				yield return CommonMethods.GenerateMessage.FieldIsNotBrush(LabelColumnBrush);

			if (CommonMethods.Check.FieldIsEmpty(ComboBoxColumnBoard))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelColumnBoard);
		}

		public void ActionBeforeTrueDialogResultClose() {
			Table = new Column {
				Header = TextBoxColumnHeader.Text,
				Brush = TextBoxColumnBrush.Text,
				BoardId = boardNames[(string)ComboBoxColumnBoard.SelectedItem]
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