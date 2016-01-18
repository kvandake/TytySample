
using UIKit;

namespace TytySample
{
	public partial class DashboardView : UIViewController
	{

		DashboardViewModel viewModel;

		public DashboardView () : base ("DashboardView", null)
		{
			viewModel = new DashboardViewModel ();
			Title = viewModel.Title;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ScheduleBarButton.Clicked += (sender, e) => viewModel.ScheduleCommand.Execute (this);
			AboutBarButton.Clicked += (sender, e) => viewModel.AboutCommand.Execute (this);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}


