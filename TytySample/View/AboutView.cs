using UIKit;

namespace TytySample
{
	public partial class AboutView : UIViewController
	{

		AboutViewModel viewModel;

		public AboutView () : base ("AboutView", null)
		{
			viewModel = new AboutViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			AppInfoLabel.Text = viewModel.AppInfoText;
			View.AddGestureRecognizer (new UITapGestureRecognizer(tap => viewModel.CloseCommand.Execute(this)));
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}


