using System;

using Foundation;
using UIKit;

namespace TytySample
{

	/// <summary>
	/// Ячейка с расширяемым текстом (с помощью Auto Layout)
	/// </summary>
	public partial class ExtendedTextViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ExtendedTextViewCell");
		public static readonly UINib Nib;

		static ExtendedTextViewCell ()
		{
			Nib = UINib.FromName ("ExtendedTextViewCell", NSBundle.MainBundle);
		}

		public static ExtendedTextViewCell Create ()
		{
			return (ExtendedTextViewCell)Nib.Instantiate (null, null) [0];
		}

		public ExtendedTextViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
