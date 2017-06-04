using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Windows.Tables {
	public partial class CommentWindow : IWindowWithChecking<Comment> {
		public Comment Table { get; private set; }
		private readonly Guid userId;
		private readonly Guid taskId;

		public CommentWindow(Comment comment, Guid userId, Guid taskId, bool isReadOnly) {
			this.userId = comment?.UserId ?? userId;
			this.taskId = comment?.TaskId ?? taskId;
			InitializeComponent();

			LoadWindowData(comment, isReadOnly);
		}
		private void LoadWindowData(Comment comment, bool isReadOnly) {
			CommonMethods.Set.ReadOnly(LabelCommentContent, isReadOnly);
			CommonMethods.Set.ReadOnly(TextBoxCommentContent, isReadOnly);
			if (comment == null)
				return;

			TextBoxCommentContent.Text = comment.Content;
		}

		public IEnumerable<string> GetErrors() {
			return Enumerable.Empty<string>();
		}

		public void ActionBeforeTrueDialogResultClose() {
			Table = new Comment {
				Content = TextBoxCommentContent.Text,
				UserId = userId,
				TaskId = taskId
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