using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Controls {
	public partial class BoardControl {
		private readonly HttpClientProvider httpClientProvider;
		private Board thisBoard;

		public event EventHandler AddColumn;
		public event EventHandler AddBoard;
		public event EventHandler EditBoard;
		public event EventHandler DeleteBoard;

		public BoardControl(HttpClientProvider httpClientProvider) {
			this.httpClientProvider = httpClientProvider;
			InitializeComponent();

			AddColumn += (sender, args) => LoadBoard(thisBoard?.Name);
		}

		public void LoadBoard(string boardName) {
			thisBoard = null;
			StackPanelColumnControls.Children.Clear();
			if (string.IsNullOrEmpty(boardName))
				return;

			thisBoard = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetWithUsingFilters(boardName).First());
			if (thisBoard == null)
				return;

			var columns = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseColumnReader().GetFromBoard(thisBoard.BoardId));
			if (columns?.Any() != true)
				return;

			foreach (var column in columns)
				StackPanelColumnControls.Children.Add(CreateColumnControl(column, boardName));
			((ColumnControl)StackPanelColumnControls.Children[StackPanelColumnControls.Children.Count - 1]).BorderThickness = new Thickness(3, 2, 3, 0);
		}
		private ColumnControl CreateColumnControl(Column column, string boardName) {
			var columnControl = new ColumnControl(httpClientProvider, column);
			columnControl.AddColumn += (sender, args) => LoadBoard(boardName);
			columnControl.EditColumn += (sender, args) => LoadBoard(boardName);
			columnControl.DeleteColumn += (sender, args) => LoadBoard(boardName);

			return columnControl;
		}

		private void MenuItemAddColumn_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((column, isReadOnly) => new ColumnWindow(column, GetBoardNames(), isReadOnly), httpClientProvider.GetDatabaseColumnEditor(), () => AddColumn?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemAddBoard_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((board, isReadOnly) => new BoardWindow(board, isReadOnly), httpClientProvider.GetDatabaseBoardEditor(), () => AddBoard?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemEditBoard_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Edit(thisBoard, (board, isReadOnly) => new BoardWindow(board, isReadOnly), httpClientProvider.GetDatabaseBoardEditor(), board => board.BoardId, () => EditBoard?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemDeleteBoard_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(thisBoard, httpClientProvider.GetDatabaseBoardEditor(), board => board.BoardId, () => DeleteBoard?.Invoke(this, default(EventArgs)));
		}

		private Dictionary<string, Guid> GetBoardNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetAll().ToDictionary(board => board.Name, board => board.BoardId));
		}
	}
}