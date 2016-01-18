using UIKit;
using System.Collections.Generic;

namespace TytySample
{
	public class CitiesScheduleView : UITableViewController, IUISearchResultsUpdating
	{

		const string SearchCellId = "SearchCellId";

		readonly CitiesScheduleViewModel viewModel;
	
		bool _showAll;

		public CitiesScheduleView (bool showAll,string context, IList<SectionStation> stations):base(UITableViewStyle.Grouped)
		{
			viewModel = new CitiesScheduleViewModel (context, stations);
			_showAll = showAll;
		}

		public void UpdateSearchResultsForSearchController (UISearchController searchController)
		{
			//костыль для отображения все станций или для отображения пустоты при инициализиции поиска
				if (_showAll && searchController.SearchResultsController.View.Hidden) {
				searchController.SearchResultsController.View.Hidden = false;
			}
			viewModel.UpdateFilteredStations (searchController.SearchBar.Text);
			TableView.ReloadData ();
		}
			
		/// <summary>
		/// Биндинг ячеек
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var dataSection = viewModel.FilteredStations [indexPath.Section];
			var data = dataSection.Stations [indexPath.Row];
			var cell = tableView.DequeueReusableCell (SearchCellId) ?? new UITableViewCell(UITableViewCellStyle.Subtitle,SearchCellId);
			cell.TextLabel.Text = data.StationTitle;
			cell.DetailTextLabel.Text = data.RegionTitle;
			cell.Accessory = UITableViewCellAccessory.DetailButton;
			return cell;
		}


		public override void AccessoryButtonTapped (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			viewModel.ShowInfoCommand.Execute (indexPath);
		}

	

		public override System.nint NumberOfSections (UITableView tableView)
		{
			return viewModel.FilteredStations.Count;
		}

		public override string TitleForHeader (UITableView tableView, System.nint section)
		{
			return viewModel.FilteredStations [(int)section].ToString ();
		}

		public override System.nint RowsInSection (UITableView tableView, System.nint section)
		{
			return viewModel.FilteredStations [(int)section].Stations.Count;
		}



		/// <summary>
		/// Выбор станции
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var searchVc = ParentViewController as UISearchController;
			if (searchVc != null) {
				var station = viewModel.FilteredStations [indexPath.Section].Stations [indexPath.Row];
				viewModel.GoBackWithresultCommand.Execute (station);
			}
			tableView.DeselectRow (indexPath, true);
		}

	}
}

