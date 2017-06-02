using System.Windows;
using System.Windows.Media;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.AdditionalControls {
	public partial class TaskControl {
		public TaskControl(Task task, Brush taskControlBrush, string developerName) {
			InitializeComponent();
			HorizontalAlignment = HorizontalAlignment.Center;

			BorderBrush = taskControlBrush;
			TextBlockHeader.Text = task.Header;
			TextBlockDescription.Text = task.Description;
			LabelBranch.Content = task.Branch;
			LabelState.Content = task.State;
			LabelPriority.Content = task.Priority;
			LabelDeveloperName.Content = developerName;
		}
	}
}
