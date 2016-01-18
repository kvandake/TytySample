using System;

using Foundation;
using UIKit;

namespace TytySample
{


	/// <summary>
	/// ячейка с вводом текста
	/// </summary>
	public partial class TextFieldViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("TextFieldViewCell");
		public static readonly UINib Nib;

		public Action<string> ChangeText { get; set;}

		static TextFieldViewCell ()
		{
			Nib = UINib.FromName ("TextFieldViewCell", NSBundle.MainBundle);
		}

		public static TextFieldViewCell Create ()
		{
			var cell = (TextFieldViewCell)Nib.Instantiate (null, null) [0];
			cell.Configure ();
			return cell;
		}

		public TextFieldViewCell (IntPtr handle) : base (handle)
		{

		}

		void Configure(){
			SelectionStyle = UITableViewCellSelectionStyle.None;
			TextField.ShouldReturn = textField => {
				textField.EndEditing (true);
				return true;
			};
			TextField.EditingChanged += (s, e) => {
				if (ChangeText != null) {
					ChangeText (TextField.Text);
				}
			};
		}
	}
}
