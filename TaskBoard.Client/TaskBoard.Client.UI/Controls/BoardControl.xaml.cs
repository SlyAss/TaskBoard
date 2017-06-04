using System.Linq;
using System.Windows;
using TaskBoard.Common.Tables;

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

			StackPanelColumnControls.Children.Clear();
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

		public void Clear() {
			StackPanelColumnControls.Children.Clear();
		}
	}
}