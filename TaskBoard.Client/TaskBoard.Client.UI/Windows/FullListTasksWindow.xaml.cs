using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TaskBoard.Client.UI.Extensions;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows {
	public partial class FullListTasksWindow {
		private readonly HttpClientProvider httpClientProvider;
		private readonly Board board;
		private Task SelectedTask => (Task)DataGridTasks.SelectedItem;

		public FullListTasksWindow(HttpClientProvider httpClientProvider, string boardName) {
			InitializeComponent();
			this.httpClientProvider = httpClientProvider;

			board = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetWithUsingFilters(boardName).First());
			if (board == null)
				return;

			DataGridTasks.LoadTable(typeof(Task));
			ReloadDataGridTasksItemsSource();
		}
		private void ReloadDataGridTasksItemsSource() {
			DataGridTasks.ItemsSource = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseTaskReader().GetFromBoard(board.BoardId));
		}

		private void ButtonOk_OnClick(object sender, RoutedEventArgs e) {
			Close();
		}

		private void DataGridTasks_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
			CommonMethods.WorkWithTables.View(SelectedTask, (task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly));
		}
		private void MenuItemAddTask_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly), httpClientProvider.GetDatabaseTaskEditor(), ReloadDataGridTasksItemsSource);
		}
		private void MenuItemEditTask_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Edit(SelectedTask, (task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly), httpClientProvider.GetDatabaseTaskEditor(), task => task.TaskId, ReloadDataGridTasksItemsSource);
		}
		private void MenuItemDeleteTask_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(SelectedTask, httpClientProvider.GetDatabaseTaskEditor(), task => task.TaskId, ReloadDataGridTasksItemsSource);
		}

		private Dictionary<string, Guid> GetUserNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetAll().ToDictionary(user => user.Login, user => user.UserId));
		}
		private Dictionary<string, Guid> GetColumnNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseColumnReader().GetFromBoard(board.BoardId).ToDictionary(column => column.Header, column => column.ColumnId));
		}
	}
}