using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Client.UI.Controls;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows.Tables {
	public partial class TaskWindow : IWindowWithChecking<Task> {
		public Task Table { get; private set; }
		private readonly HttpClientProvider httpClientProvider;
		private readonly Dictionary<string, Guid> userNames;
		private readonly Dictionary<string, Guid> columnNames;
		private readonly Guid taskBoardId;
		private readonly Task thisTask;

		public TaskWindow(HttpClientProvider httpClientProvider, Task task, Dictionary<string, Guid> userNames, Dictionary<string, Guid> columnNames, string taskBoardName, Guid taskBoardId, bool isReadOnly) {
			InitializeComponent();
			this.httpClientProvider = httpClientProvider;
			this.userNames = userNames;
			this.columnNames = columnNames;
			this.taskBoardId = taskBoardId;
			thisTask = task;

			LoadWindowData(task, userNames, columnNames, taskBoardName, isReadOnly);
		}
		private void LoadWindowData(Task task, Dictionary<string, Guid> userNames, Dictionary<string, Guid> columnNames, string taskBoardName, bool isReadOnly) {
			ComboBoxTaskDeveloper.ItemsSource = new[] { string.Empty }.Concat(userNames.Keys).ToArray();
			ComboBoxTaskReviewer.ItemsSource = new[] { string.Empty }.Concat(userNames.Keys).ToArray();
			ComboBoxTaskColumn.ItemsSource = new[] { string.Empty }.Concat(columnNames.Keys).ToArray();
			ComboBoxTaskState.ItemsSource = Enum.GetNames(typeof(State));
			ComboBoxTaskPriority.ItemsSource = Enum.GetNames(typeof(Priority));
			DatePickerTaskCreateDateTime.SelectedDate = task?.CreateDateTime ?? DateTime.Now;
			ComboBoxTaskState.SelectedItem = task?.State.ToString() ?? default(State).ToString();
			ComboBoxTaskPriority.SelectedItem = task?.Priority.ToString() ?? default(Priority).ToString();
			TextBoxTaskBoard.Text = taskBoardName;

			CommonMethods.Set.ReadOnly(LabelTaskHeader, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxTaskHeader, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskDescription, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxTaskDescription, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskDeveloper, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxTaskDeveloper, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskReviewer, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxTaskReviewer, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskColumn, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxTaskColumn, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskBoard, true);
			CommonMethods.Set.ReadOnly(TextBoxTaskBoard, true);
			CommonMethods.Set.ReadOnly(LabelTaskBranch, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxTaskBranch, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskState, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxTaskState, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskPriority, isReadOnly);
			CommonMethods.Set.ReadOnly(ComboBoxTaskPriority, isReadOnly);
			CommonMethods.Set.ReadOnly(LabelTaskCreateDateTime, true);
			CommonMethods.Set.ReadOnly(DatePickerTaskCreateDateTime, true);
			GridCommects.Visibility = task == null ? Visibility.Collapsed : Visibility.Visible;
			if (task == null)
				return;

			TextBoxTaskHeader.Text = task.Header;
			TextBoxTaskDescription.Text = task.Description;
			TextBoxTaskBranch.Text = task.Branch;
			if (task.DeveloperId != null)
				ComboBoxTaskDeveloper.SelectedItem = userNames.First(userName => userName.Value == task.DeveloperId.Value).Key;
			if (task.ReviewerId != null)
				ComboBoxTaskReviewer.SelectedItem = userNames.First(userName => userName.Value == task.ReviewerId.Value).Key;
			if (task.ColumnId != null)
				ComboBoxTaskColumn.SelectedItem = columnNames.First(columnName => columnName.Value == task.ColumnId.Value).Key;
			UpdateListComments();
		}
		private void UpdateListComments() {
			var comments = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseCommentReader().GetFromTask(thisTask.TaskId));
			if (comments == null)
				return;

			StackPanelComments.Children.Clear();
			foreach (var commentControl in comments.Select(comment => new CommentControl(httpClientProvider, comment))) {
				commentControl.AddComment += (sender, args) => UpdateListComments();
				commentControl.EditComment += (sender, args) => UpdateListComments();
				commentControl.DeleteComment += (sender, args) => UpdateListComments();
				StackPanelComments.Children.Add(commentControl);
			}
		}

		public IEnumerable<string> GetErrors() {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxTaskHeader))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelTaskHeader);

			if (CommonMethods.Check.FieldIsEmpty(TextBoxTaskDescription))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelTaskDescription);

			if (CommonMethods.Check.FieldIsEmpty(ComboBoxTaskState))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelTaskState);

			if (CommonMethods.Check.FieldIsEmpty(ComboBoxTaskPriority))
				yield return CommonMethods.GenerateMessage.FieldIsEmpty(LabelTaskPriority);
		}

		public void ActionBeforeTrueDialogResultClose() {
			Table = new Task {
				Header = TextBoxTaskHeader.Text,
				Description = TextBoxTaskDescription.Text,
				Branch = TextBoxTaskBranch.Text,
				State = (State)Enum.Parse(typeof(State), (string)ComboBoxTaskState.SelectedItem),
				Priority = (Priority)Enum.Parse(typeof(Priority), (string)ComboBoxTaskPriority.SelectedItem),
				DeveloperId = string.IsNullOrEmpty((string)ComboBoxTaskDeveloper.SelectedItem) ? (Guid?)null : userNames[(string)ComboBoxTaskDeveloper.SelectedItem],
				ReviewerId = string.IsNullOrEmpty((string)ComboBoxTaskReviewer.SelectedItem) ? (Guid?)null : userNames[(string)ComboBoxTaskReviewer.SelectedItem],
				ColumnId = string.IsNullOrEmpty((string)ComboBoxTaskColumn.SelectedItem) ? (Guid?)null : columnNames[(string)ComboBoxTaskColumn.SelectedItem],
				BoardId = taskBoardId
			};
		}
		public void ActionBeforeFalseDialogResultClose() {
		}

		private void ButtonSendComment_OnClick(object sender, RoutedEventArgs e) {
			if (CommonMethods.Check.FieldIsEmpty(TextBoxCommentContent)) {
				CommonMethods.ShowMessageBox.Error("Необходимо написать комментарий");
				return;
			}

			var userId = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetWithUsingFilters(httpClientProvider.Login))?.First()?.UserId;
			if (userId == null)
				return;

			CommonMethods.SafeRunMethod.WithoutReturn(() => httpClientProvider.GetDatabaseCommentEditor().Add(new Comment {
				Content = TextBoxCommentContent.Text,
				UserId = userId.Value,
				TaskId = thisTask.TaskId
			}));

			TextBoxCommentContent.Text = string.Empty;
			UpdateListComments();
		}

		private void ButtonOk_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.TrueDialogResult(this);
		}
		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.CloseWindow.FalseDialogResult(this);
		}
	}
}