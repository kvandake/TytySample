using UIKit;
using Foundation;
using CoCore.Base;

namespace TytySample
{
	[Register("ScheduleView")]
	public class ScheduleView : UITableViewController,IReturnWithResult
	{

		ScheduleViewModel viewModel;
		ScheduleTableSource sourceTable;

		public ScheduleView () : base (UITableViewStyle.Grouped)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			viewModel = new ScheduleViewModel ();
			Title = viewModel.Title;
			NavigationItem.LeftBarButtonItem = TouchConstant.LeftCancel ((s, e) => viewModel.CloseCommand.Execute (this));
			sourceTable = new ScheduleTableSource (TableView);
			sourceTable.DataSource = viewModel.Items;
			sourceTable.CreateCell = CreatorCell;
			TableView.Source = sourceTable;
			viewModel.ShowSelectDateHandler += (s, index) => {
				var indexPath = index as NSIndexPath;
				if (indexPath != null) {
					sourceTable.DateDisplayInlineDatePickerForRowAtIndexPath (indexPath);
				}
			};
			sourceTable.ChangeDate = (s, e) => {
				viewModel.DateSchedule = e;
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}


		/// <summary>
		/// Метод для создания ячеек
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="item">Item.</param>
		/// <param name="index">Index.</param>
		UITableViewCell CreatorCell(UITableView tableView, GroupCell item, NSIndexPath index){
			var cell = tableView.DequeueReusableCell (item.Tag ?? item.ToString ());
			switch (item.Tag) {
			case ScheduleViewModel.CellType_FromSchedule:
				TextFieldViewCell fromCell;
				if(cell == null){
					fromCell = TextFieldViewCell.Create ();
					fromCell.TextField.Placeholder = item.SecondaryText;
					fromCell.TextField.Started += (s, e) => viewModel.FromScheduleCommand.Execute (this);
					fromCell.TextField.InputView = new UIView ();
					cell = fromCell;
				}else{
					fromCell = cell as TextFieldViewCell;
				}
				fromCell.TextField.Text = item.PrimaryText;
				break;
			case ScheduleViewModel.CellType_ToSchedule:
				TextFieldViewCell toCell;
				if(cell == null){
					toCell = TextFieldViewCell.Create ();
					toCell.TextField.Placeholder = item.SecondaryText;
					toCell.TextField.Started += (s, e) => viewModel.ToScheduleCommand.Execute (this);
					toCell.TextField.InputView = new UIView ();
					cell = toCell;
				}else{
					toCell = cell as TextFieldViewCell;
				}
				toCell.TextField.Text = item.PrimaryText;
				break;	
			case ScheduleViewModel.CellType_DateSchedule:
				TextRangeViewCell dateCell;
				if (cell == null) {
					dateCell = TextRangeViewCell.Create (item.Tag);
					cell = dateCell;
				} else {
					dateCell = cell as TextRangeViewCell;
				}
				dateCell.TextLabel.Text = item.PrimaryText;
				dateCell.TextLabel.TextColor = UIColor.FromRGB (199, 199, 205);
				dateCell.AccessorText = item.SecondaryText;
				break;
			case ScheduleViewModel.CellType_Switch:
				SwitchRangeViewCell switchCell;
				if (cell == null) {
					switchCell = SwitchRangeViewCell.Create (item.Tag);
					switchCell.ChangeSwitch += (s, e) => viewModel.UpdateShowAll(e);
					cell = switchCell;
				} else {
					switchCell = cell as SwitchRangeViewCell;
				}
				switchCell.TextLabel.Text = item.PrimaryText;
				switchCell.AccessorOn = (bool)item.Data;
				break;
			}
			return cell;
		}


		/// <summary>
		/// передача значений во вьюшку
		/// </summary>
		/// <returns><c>true</c>, if with result was returned, <c>false</c> otherwise.</returns>
		/// <param name="intent">Intent.</param>
		#region IReturnWithResult implementation
		public bool ReturnWithResult (Intent intent)
		{
			return viewModel.ReturnWithResult (intent);
		}
		#endregion
	}
}


