using System;

using Foundation;
using UIKit;

namespace TytySample
{


	/// <summary>
	/// ячейка с выбором даты
	/// </summary>
	public partial class SelectDateViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("SelectDateViewCell");
		public static readonly UINib Nib;

		public event EventHandler<DateTime> ChangeDate;

		static SelectDateViewCell ()
		{
			Nib = UINib.FromName ("SelectDateViewCell", NSBundle.MainBundle);
		}

		public static SelectDateViewCell Create ()
		{
			return (SelectDateViewCell)Nib.Instantiate (null, null) [0];
		}




		public SelectDateViewCell (IntPtr handle) : base (handle)
		{
		}

		public void Config(){
			DatePicker.ValueChanged += (s, e) => {
				var picker = s as UIDatePicker;
				if (picker != null) {
					var iosDate = (DateTime)picker.Date;
					var convertDate = DateTime.SpecifyKind(iosDate.ToLocalTime(),DateTimeKind.Utc);
					OnChangeDate (convertDate);
				}
			};
		}

		protected virtual void OnChangeDate (DateTime e)
		{
			var handler = ChangeDate;
			if (handler != null)
				handler (this, e);
		}
	}
}
