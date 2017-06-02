using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace TaskBoard.Client.UI.AdditionalControls {
	public partial class ColumnControl {
		public ColumnControl(HttpClientProvider httpClientProvider, Guid columnId) {
			InitializeComponent();

			CreateColumn(httpClientProvider, columnId);
		}
		private void CreateColumn(HttpClientProvider httpClientProvider, Guid columnId) {
			var column = httpClientProvider.GetDatabaseColumnReader().GetById(columnId);

			LabelColumnHeader.Content = column.Header;
			foreach (var task in httpClientProvider.GetDatabaseTaskReader().GetFromColumn(column.BoardId, columnId)) {
				var developerName = task.DeveloperId != null ? httpClientProvider.GetDatabaseUserReader().GetById(task.DeveloperId.Value).Login : string.Empty;
				StackPanelTaskControls.Children.Add(new TaskControl(task, (Brush)new BrushConverter().ConvertFromString(column.Brush), developerName));
			}
		}
	}
}
