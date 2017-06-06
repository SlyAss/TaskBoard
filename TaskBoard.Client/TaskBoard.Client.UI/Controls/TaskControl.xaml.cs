using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Controls {
	public partial class TaskControl {
		private readonly HttpClientProvider httpClientProvider;
		private readonly Task thisTask;

		public event EventHandler AddTask;
		public event EventHandler EditTask;
		public event EventHandler DeleteTask;

		public TaskControl(HttpClientProvider httpClientProvider, Task task, Brush taskControlBrush, string developerName) {
			InitializeComponent();
			this.httpClientProvider = httpClientProvider;
			thisTask = task;
			HorizontalAlignment = HorizontalAlignment.Center;

			CreateTaskControl(task, taskControlBrush, developerName);
		}
		private void CreateTaskControl(Task task, Brush taskControlBrush, string developerName) {
			BorderBrush = taskControlBrush;
			TextBlockHeader.Text = task.Header;
			TextBlockDescription.Text = task.Description;
			LabelBranch.Content = task.Branch;
			LabelState.Content = task.State;
			LabelPriority.Content = task.Priority;
			LabelDeveloperName.Content = developerName;
		}

		private void ViewTask_OnClick(object sender, MouseButtonEventArgs e) {
			var board = GetThisBoard();
			CommonMethods.WorkWithTables.View(thisTask, (task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly));
		}
		private void MenuItemAddTask_OnClick(object sender, RoutedEventArgs e) {
			var board = GetThisBoard();
			CommonMethods.WorkWithTables.Add((task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly), httpClientProvider.GetDatabaseTaskEditor(), () => AddTask?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemEditTask_OnClick(object sender, RoutedEventArgs e) {
			var board = GetThisBoard();
			CommonMethods.WorkWithTables.Edit(thisTask, (task, isReadOnly) => new TaskWindow(task, GetUserNames(), GetColumnNames(), board.Name, board.BoardId, isReadOnly), httpClientProvider.GetDatabaseTaskEditor(), task => task.TaskId, () => EditTask?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemDeleteTask_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(thisTask, httpClientProvider.GetDatabaseTaskEditor(), task => task.TaskId, () => DeleteTask?.Invoke(this, default(EventArgs)));
		}

		private Dictionary<string, Guid> GetUserNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetAll().ToDictionary(user => user.Login, user => user.UserId));
		}
		private Dictionary<string, Guid> GetColumnNames() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseColumnReader().GetFromBoard(thisTask.BoardId).ToDictionary(column => column.Header, column => column.ColumnId));
		}
		private Board GetThisBoard() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetById(thisTask.BoardId));
		}
	}
}