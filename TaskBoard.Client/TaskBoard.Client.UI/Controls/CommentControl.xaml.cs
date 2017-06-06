using System;
using System.Linq;
using System.Windows;
using TaskBoard.Client.UI.Windows.Tables;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Controls {
	public partial class CommentControl {
		private readonly HttpClientProvider httpClientProvider;
		private readonly Comment thisComment;

		public event EventHandler AddComment;
		public event EventHandler EditComment;
		public event EventHandler DeleteComment;

		public CommentControl(HttpClientProvider httpClientProvider, Comment comment) {
			InitializeComponent();
			this.httpClientProvider = httpClientProvider;
			thisComment = comment;

			LoadCommect(httpClientProvider, comment);
		}
		private void LoadCommect(HttpClientProvider httpClientProvider, Comment comment) {
			var user = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetById(comment.UserId));
			if (user == null)
				return;

			LabelUserName.Content = user.Login;
			LabelCreateDateTime.Content = comment.CreateDateTime.ToString("dd.MM.yyyy HH:mm:ss");
			TextBoxCommentContent.Text = comment.Content;
		}

		private void MenuItemAddComment_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Add((comment, isReadOnly) => new CommentWindow(comment, GetThisUserId(), thisComment.TaskId, isReadOnly), httpClientProvider.GetDatabaseCommentEditor(), () => AddComment?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemEditComment_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Edit(thisComment, (comment, isReadOnly) => new CommentWindow(comment, GetThisUserId(), thisComment.TaskId, isReadOnly), httpClientProvider.GetDatabaseCommentEditor(), comment => comment.CommentId, () => EditComment?.Invoke(this, default(EventArgs)));
		}
		private void MenuItemDeleteComment_OnClick(object sender, RoutedEventArgs e) {
			CommonMethods.WorkWithTables.Delete(thisComment, httpClientProvider.GetDatabaseCommentEditor(), comment => comment.CommentId, () => DeleteComment?.Invoke(this, default(EventArgs)));
		}

		private Guid GetThisUserId() {
			return CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetWithUsingFilters(httpClientProvider.Login).First().UserId);
		}
	}
}