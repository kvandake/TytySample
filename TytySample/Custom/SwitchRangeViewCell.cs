using System;
using UIKit;
using CoreGraphics;

namespace TytySample
{

	/// <summary>
	/// Стандартная яцейка с аксессором - UISwitch
	/// </summary>
	public class SwitchRangeViewCell: UITableViewCell
	{
		UISwitch accessorSwitch;
		bool accessorOn;


		public event EventHandler<bool> ChangeSwitch;

		public UISwitch AccessorSwitch {
			get {
				return accessorSwitch;
			}
		}



		public bool AccessorOn {
			get {
				return accessorOn;
			}
			set {
				accessorOn = value;
				accessorSwitch.On = value;
			}
		}


		public static SwitchRangeViewCell Create (string tag)
		{
			var cell = new SwitchRangeViewCell (UITableViewCellStyle.Default, tag);
			cell.Configure ();
			return cell;
		}

		public SwitchRangeViewCell (IntPtr handle) : base (handle)
		{
		}

		public SwitchRangeViewCell (UITableViewCellStyle style, string tag) : base (style,tag)
		{
		}


		void Configure(){
			accessorSwitch = new UISwitch (CGRect.Empty);
			AccessoryView = accessorSwitch;
			accessorSwitch.ValueChanged += (s, e) => OnChangeSwitch (accessorSwitch.On);
		}

		protected virtual void OnChangeSwitch (bool e)
		{
			var handler = ChangeSwitch;
			if (handler != null)
				handler (this, e);
		}
	}
}

