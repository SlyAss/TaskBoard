using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables.Attributes;

namespace TaskBoard.Client.UI.Extensions {
	public static class DataGridExtensions {
		private static readonly Dictionary<Type, Func<string, string, DataGridColumn>> generateColumn =
			new Dictionary<Type, Func<string, string, DataGridColumn>> {
				{ typeof(string), GenerateTextColumn },
				{ typeof(State), GenerateStateColumn },
				{ typeof(Priority), GeneratePriorityColumn },
				{ typeof(DateTime), GenerateDataTimeColumn }
			};

		public static void LoadTable(this DataGrid dataGrid, Type type, bool isReadOnly = true) {
			dataGrid.SelectionMode = DataGridSelectionMode.Single;
			dataGrid.CanUserReorderColumns = false;
			dataGrid.AutoGenerateColumns = false;
			dataGrid.CanUserDeleteRows = false;
			dataGrid.IsReadOnly = isReadOnly;
			dataGrid.CanUserAddRows = false;
			dataGrid.ItemsSource = null;
			dataGrid.Columns.Clear();

			var columns = type.GetProperties()
				.Select(propertyInfo => new {
					propertyInfo.Name,
					propertyInfo.PropertyType,
					Attribute = propertyInfo.GetCustomAttributes<HeaderColumnAttribute>().FirstOrDefault()
				})
				.Where(x => x.Attribute != null && generateColumn.ContainsKey(x.PropertyType))
				.Select(x => generateColumn[x.PropertyType](x.Attribute.HeaderColumn, x.Name))
				.ToArray();
			columns.Last().Width = new DataGridLength(1, DataGridLengthUnitType.Star);

			foreach (var column in columns)
				dataGrid.Columns.Add(column);
		}

		private static DataGridColumn GenerateTextColumn(string header, string bindingName) {
			return new DataGridTextColumn {
				Header = header,
				IsReadOnly = true,
				Binding = new Binding(bindingName)
			};
		}
		private static DataGridColumn GenerateDataTimeColumn(string header, string bindingName) {
			return new DataGridTextColumn {
				Header = header,
				IsReadOnly = true,
				Binding = new Binding(bindingName) {
					StringFormat = "dd.MM.yyyy HH:mm:ss"
				}
			};
		}
		private static DataGridColumn GeneratePriorityColumn(string header, string bindingName) {
			return new DataGridTextColumn {
				Header = header,
				IsReadOnly = true,
				Binding = new Binding(bindingName)
			};
		}
		private static DataGridColumn GenerateStateColumn(string header, string bindingName) {
			return new DataGridTextColumn {
				Header = header,
				IsReadOnly = true,
				Binding = new Binding(bindingName)
			};
		}
	}
}