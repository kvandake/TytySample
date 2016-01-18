using System;
using CoCore.iOS;
using UIKit;
using Foundation;
using System.Globalization;

namespace TytySample
{

	/// <summary>
	/// Schedule table source. (логика взята из https://developer.apple.com/library/ios/samplecode/DateCell/Introduction/Intro.html)
	/// </summary>
	public class ScheduleTableSource : GroupTableSource
	{
		static readonly nint kDatePickerTag = 99;
		NSIndexPath pickerIndexPath;
		nfloat pickerCellRowHeight;
		nint currentPicker;

		public EventHandler<DateTime> ChangeDate {get;set;}

		public ScheduleTableSource (UITableView tableView):base(tableView)
		{
			Initialize ();
		}

		void Initialize(){
			var dateCell = SelectDateViewCell.Create ();
			pickerCellRowHeight = dateCell.Bounds.Height;
		}


		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (HasInlineDatePicker && pickerIndexPath.Section == section) {
				var count = base.RowsInSection (tableview, section);
				return ++count;
			}
			return base.RowsInSection (tableview, section);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			string cellId = null;
			if (IndexPathHasPicker (indexPath)) {
				if (currentPicker == 21) {
					cellId = SelectDateViewCell.Key.ToString ();
				}
			}
			// if we have a date picker open whose cell is above the cell we want to update,
			// then we have one more cell than the model allows
			//
			nint modelRow = indexPath.Row;
			if (pickerIndexPath != null && pickerIndexPath.Section == indexPath.Section && pickerIndexPath.Row <= indexPath.Row) {
				modelRow--;
				indexPath = NSIndexPath.FromRowSection (modelRow, indexPath.Section);
			}
			if (cellId == null) {
				var cell = base.GetCell (tableView, indexPath);
				return cell;
			} else if (cellId == SelectDateViewCell.Key.ToString ()) {
				var dateCell = tableView.DequeueReusableCell (cellId) as SelectDateViewCell;
				if (dateCell == null) {
					dateCell = SelectDateViewCell.Create ();
					dateCell.Config ();
					dateCell.ChangeDate += ChangeDate;
				}
				return dateCell;
			}
			return null;
		}


		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return IndexPathHasPicker(indexPath) ? pickerCellRowHeight : tableView.RowHeight;
		}


		#region DatePicker



		bool IndexPathHasPicker(NSIndexPath indexPath){
			return HasInlineDatePicker && pickerIndexPath.Row == indexPath.Row && pickerIndexPath.Section == indexPath.Section;
		}

		bool HasInlineDatePicker {
			get{
				return pickerIndexPath != null;
			}
		}

		/// <summary>
		/// Determines if the given indexPath has a cell below it with a UIDatePicker.
		/// </summary>
		/// <returns><c>true</c> if this instance has picker for index path the specified indexPath; otherwise, <c>false</c>.</returns>
		/// <param name="indexPath">indexPath The indexPath to check if its cell has a UIDatePicker below it</param>
		bool DateHasPickerForIndexPath(NSIndexPath indexPath)
		{
			bool hasDatePicker;
			nint targetedRow = indexPath.Row;
			targetedRow++;
			UITableViewCell checkDatePickerCell = TableView.CellAt (NSIndexPath.FromRowSection (targetedRow, indexPath.Section)); 
			if (checkDatePickerCell == null) {
				return false;
			}
			var checkDatePicker = (UIDatePicker)checkDatePickerCell.ViewWithTag (kDatePickerTag);
			hasDatePicker = (checkDatePicker != null);
			return hasDatePicker;
		}


		/// <summary>
		/// Adds or removes a UIDatePicker cell below the given indexPath.
		/// </summary>
		/// <param name="indexPath">indexPath The indexPath to reveal the UIDatePicker.</param>
		void DateToggleDatePickerForSelectedIndexPath(NSIndexPath indexPath)
		{
			TableView.BeginUpdates ();
			NSIndexPath[] indexPaths = { NSIndexPath.FromRowSection (indexPath.Row + 1, indexPath.Section) };
			// check if 'indexPath' has an attached date picker below it
			if (DateHasPickerForIndexPath (indexPath)) {
				// found a picker below it, so remove it
				TableView.DeleteRows (indexPaths, UITableViewRowAnimation.Fade);
			} else {
				// didn't find a picker below it, so we should insert it
				TableView.InsertRows (indexPaths, UITableViewRowAnimation.Fade);
			}
			TableView.EndUpdates ();
		}

		/// <summary>
		/// Updates the UIDatePicker's value to match with the date of the cell above it.
		/// </summary>
		void UpdateDatePicker()
		{
			if (pickerIndexPath != null) {
				UITableViewCell associatedDatePickerCell = TableView.CellAt (pickerIndexPath);
				var targetedDatePicker = (UIDatePicker)associatedDatePickerCell.ViewWithTag (kDatePickerTag);
				if (targetedDatePicker != null) {
					var dateCell = DataSource[0][2];
					if (dateCell != null) {
						CultureInfo provider = CultureInfo.InvariantCulture;
						DateTime date;
						if (!DateTime.TryParseExact (dateCell.SecondaryText, ScheduleViewModel.PresentDateFormat, provider, DateTimeStyles.AdjustToUniversal, out date)) {
							date = DateTime.UtcNow;
						}
						targetedDatePicker.SetDate (date.ToNSDate (), false);
					}
				}
			}
		}

		public void DateDisplayInlineDatePickerForRowAtIndexPath(NSIndexPath indexPath)
		{
			currentPicker = 21;
			// display the date picker inline with the table content
			TableView.BeginUpdates();
			bool before = false; // indicates if the date picker is below "indexPath", help us determine which row to reveal
			bool sameCellClicked = false;
			if (HasInlineDatePicker)
			{
				before = pickerIndexPath.Row < indexPath.Row;
				sameCellClicked = pickerIndexPath.Row - 1 == indexPath.Row;
			}

			// remove any date picker cell if it exists
			if (HasInlineDatePicker) {
				TableView.DeleteRows (new[] { NSIndexPath.FromItemSection (pickerIndexPath.Row,indexPath.Section) }, UITableViewRowAnimation.Fade);
				pickerIndexPath = null;

			}
			if (!sameCellClicked) {
				// hide the old date picker and display the new one
				nint rowToReveal = (before ? indexPath.Row - 1 : indexPath.Row);
				NSIndexPath indexPathToReveal = NSIndexPath.FromRowSection (rowToReveal, indexPath.Section);
				DateToggleDatePickerForSelectedIndexPath (indexPathToReveal);
				pickerIndexPath = NSIndexPath.FromRowSection (indexPathToReveal.Row + 1, indexPath.Section);
			}

			// always deselect the row containing the start or end date
			TableView.DeselectRow(indexPath,true);
			TableView.EndUpdates();
			// inform our date picker of the current date to match the current cell
			UpdateDatePicker();
		}


		#endregion

	}
}

