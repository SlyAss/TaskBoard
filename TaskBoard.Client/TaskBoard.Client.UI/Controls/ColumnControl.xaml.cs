using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Controls {
	public partial class ColumnControl {
		private readonly HttpClientProvider httpClientProvider;
		private readonly Column thisColumn;

		public event EventHandler AddTask;
		public event EventHandler AddColumn;
		public event EventHandler EditColumn;
		public event EventHandler DeleteColumn;

		public ColumnControl(HttpClientProvider httpClientProvider, Column column) {
			InitializeComponent();
			AddTask += (sender, args) => CreateColumn(httpClientProvider, column);
			this.httpClientProvider = httpClientProvider;
			thisColumn = column;

			CreateColumn(httpClientProvider, column);
		}
		private void CreateColumn(HttpClientProvider httpClientProvider, Column column) {
			var tasks = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseTaskReader().GetFromColumn(column.BoardId, column.ColumnId));
			if (tasks == null)
				return;

			LabelColumnHeader.Content = column.Header;
			StackPanelTaskControls.Children.Clear();
			foreach (var task in tasks)
				StackPanelTaskControls.Children.Add(CreateTaskControl(httpClientProvider, column, task));
		}

		private TaskControl CreateTaskControl(HttpClientProvider httpClientProvider, Column column, Task task) {
			var developerName = task.DeveloperId == null ? string.Empty : CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetById(task.DeveloperId.Value).Login);

			var taskControl = new TaskControl(httpClientProvider, task, (Brush)new BrushConverter().ConvertFromString(column.Brush), developerName);
			taskControl.AddTask += (sender, args) => CreateColumn(httpClientProvider, thisColumn);
			taskControl.EditTask += (sender, args) => CreateColumn(httpClientProvider, thisColumn);
			taskControl.DeleteTask += (sender, args) => CreateColumn(httpClientProvider, thisColumn);

			return taskControl;
		}

		private void ViewColumn_OnClick(object sender, MouseButtonEventArgs e) {
			CommonMethods.WorkWithTables.View(thisColumn, (column, isReadOnly) => new ColumnWindow(column, GetBoardNames(), isReadOnly));
		}
		private void MenuItemAddTask_OnClick(object sender, RoutedEventArgs e) {
			var board = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetById(thisColumn.BoardId));
			CommonMethods.WorkWithTables.Add((task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly), httpClientProvider.GetDatabaseTaskEditor(), () => AddTask?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemAddColumn_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((column, isReadOnly) => new ColumnWindow(column, GetBoardNames(), isReadOnly), httpClientProvider.GetDatabaseColumnEditor(), () => AddColumn?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemEditColumn_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Edit(thisColumn, (column, isReadOnly) => new ColumnWindow(column, GetBoardNames(), isReadOnly), httpClientProvider.GetDatabaseColumnEditor(), column => column.ColumnId, () => EditColumn?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemDeleteColumn_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(thisColumn, httpClientProvider.GetDatabaseColumnEditor(), column => column.ColumnId, () => DeleteColumn?.Invoke(this, default(EventArgs)));
		}

		private Dictionary<string, Guid> GetBoardNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetAll().ToDictionary(board => board.Name, board => board.BoardId));
		}
		private Dictionary<string, Guid> GetUserNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetAll().ToDictionary(user => user.Login, user => user.UserId));
		}
		private Dictionary<string, Guid> GetColumnNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseColumnReader().GetAll().ToDictionary(column => column.Header, column => column.ColumnId));
		}
	}
}