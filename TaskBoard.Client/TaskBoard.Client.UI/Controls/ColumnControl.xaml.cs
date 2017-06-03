using System.Windows.Media;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Controls {
	public partial class ColumnControl {
		public ColumnControl(HttpClientProvider httpClientProvider, Column column) {
			InitializeComponent();

			CreateColumn(httpClientProvider, column);
		}
		private void CreateColumn(HttpClientProvider httpClientProvider, Column column) {
			var tasks = CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseTaskReader().GetFromColumn(column.BoardId, column.ColumnId));
			if (tasks == null)
				return;

			LabelColumnHeader.Content = column.Header;
			foreach (var task in tasks) {
				var developerName = task.DeveloperId == null ? string.Empty : CommonMethods.SafeRunMethod.WithReturn(() => httpClientProvider.GetDatabaseUserReader().GetById(task.DeveloperId.Value).Login);
				StackPanelTaskControls.Children.Add(new TaskControl(task, (Brush)new BrushConverter().ConvertFromString(column.Brush), developerName));
			}
		}
	}
}