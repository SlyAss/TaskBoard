using System.Linq;
using System.Windows;

namespace TaskBoard.Client.UI.Controls {
	public partial class BoardControl {
		private HttpClientProvider httpClientProvider;

		public BoardControl() {
			InitializeComponent();
		}

		public void SetHttpClientProvider(HttpClientProvider httpClientProvider) {
			this.httpClientProvider = httpClientProvider;
		}

		public void LoadBoard(string boardName) {
			var board = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseBoardReader().GetWithUsingFilters(boardName).First());
			if (board == null)
				return;

			var columns = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseColumnReader().GetFromBoard(board.BoardId));
			if (columns == null)
				return;

			foreach (var column in columns)
				StackPanelColumnControls.Children.Add(new ColumnControl(httpClientProvider, column));
			((ColumnControl)StackPanelColumnControls.Children[StackPanelColumnControls.Children.Count - 1]).BorderThickness = new Thickness(3, 2, 3, 0);
		}

		public void Clear() {
			StackPanelColumnControls.Children.Clear();
		}
	}
}