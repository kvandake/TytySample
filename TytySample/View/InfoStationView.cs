using UIKit;
using CoCore.iOS;
using CoCore.Base;
using Foundation;

namespace TytySample
{
	public class InfoStationView  :UITableViewController
	{

		readonly InfoStationViewModel viewModel;
		GroupTableSource sourceTable;

		public InfoStationView (Station station)
		{
			viewModel = new InfoStationViewModel (station);
			Title = viewModel.Title;
			sourceTable = new GroupTableSource (TableView);
			sourceTable.DataSource = viewModel.Items;
			sourceTable.CreateCell = CreatorCell;
			TableView.Source = sourceTable;
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.TableFooterView = new UIView ();
			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 160;
			NavigationItem.LeftBarButtonItem = TouchConstant.LeftCancel ((s, e) => viewModel.CloseCommand.Execute (this));
		}

		/// <summary>
		/// Метод для создания ячеек
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="item">Item.</param>
		/// <param name="index">Index.</param>
		static UITableViewCell CreatorCell(UITableView tableView, GroupCell item, NSIndexPath index){
			var cell = tableView.DequeueReusableCell (item.Tag ?? item.ToString ());
			switch (item.Tag) {
			case InfoStationViewModel.CellType_Default:
				TextRangeViewCell defaultCell;
				if (cell == null) {
					defaultCell = TextRangeViewCell.Create (item.Tag);
					cell = defaultCell;
				} else {
					defaultCell = cell as TextRangeViewCell;
				}
				defaultCell.TextLabel.Text = item.PrimaryText;
				defaultCell.AccessorText = item.SecondaryText;
				break;
			case InfoStationViewModel.CellType_Date:
				MapViewCell mapCell;
				var station = item.Data as Station;
				if (station != null) {
					if (cell == null) {
						item.Height = (float)tableView.Bounds.Width;
						mapCell = MapViewCell.Create ();
						mapCell.Config ();
						cell = mapCell;
					} else {
						mapCell = cell as MapViewCell;
					}
					mapCell.UpdateLocation (station.Point, station.StationTitle, station.RegionTitle);
				}
				break;
			case InfoStationViewModel.CellType_ExtendedText:
				ExtendedTextViewCell extendedCell;
				if (cell == null) {
					extendedCell = ExtendedTextViewCell.Create ();
					cell = extendedCell;
				} else {
					extendedCell = cell as ExtendedTextViewCell;
				}
				extendedCell.PrimaryLabel.Text = item.PrimaryText;
				break;
			}
			return cell;
		}
	}
}

