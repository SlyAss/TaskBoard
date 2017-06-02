using System;

namespace TaskBoard.Client.UI.AdditionalControls {
	public partial class ColumnControl {
		public ColumnControl(HttpClientProvider httpClientProvider, Guid columnId) {
			InitializeComponent();

			CreateColumn(httpClientProvider, columnId);
		}
		private void CreateColumn(HttpClientProvider httpClientProvider, Guid columnId) {
			var column = httpClientProvider.GetDatabaseColumnReader().GetById(columnId);

			LabelColumnHeader.Content = column.Header;

			//foreach (var VARIABLE in httpClientProvider.GetDatabaseTaskReader().GetIF) {
				
			//}
		}
	}
}
