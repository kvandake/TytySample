using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace TytySample
{
	/// <summary>
	/// Стандартная яцейка с аксессором - UILabel
	/// </summary>
	[Register("TextRangeViewCell")]
	public class TextRangeViewCell : UITableViewCell
	{

		public static readonly NSString Key = new NSString ("TextRangeViewCell");


		UILabel accessorLabel;
		string accessorText;

		public UILabel AccessorLabel {
			get {
				return accessorLabel;
			}
		}



		public string AccessorText {
			get {
				return accessorText;
			}
			set {
				accessorText = value;
				accessorLabel.Text = value;
				accessorLabel.SizeToFit ();
			}
		}
			

		public static TextRangeViewCell Create (string tag)
		{
			var cell = new TextRangeViewCell (UITableViewCellStyle.Default, tag);
			cell.Configure ();
			return cell;
		}

		public TextRangeViewCell (IntPtr handle) : base (handle)
		{
		}

		public TextRangeViewCell (UITableViewCellStyle style, string tag) : base (style,tag)
		{
		}


		void Configure(){
			accessorLabel = new UILabel (CGRect.Empty);
			AccessoryView = accessorLabel;
		}
	}
}
