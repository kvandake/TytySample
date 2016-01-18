using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace TytySample
{
	public class CitiesViewBase : UITableViewController
	{

		public UISearchController SearchController { get;private set;}


		public UITableView TableView { get;private set;}


		public CitiesViewBase ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			View.BackgroundColor = UIColor.White;
//			TableView = new UITableView (CGRect.Empty, UITableViewStyle.Plain);
//			TableView.TranslatesAutoresizingMaskIntoConstraints = false;
//			View.AddSubview (TableView);
//			View.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-[tableview]-|",(NSLayoutFormatOptions)0,"tableview",TableView));
//			View.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-[tableview]-|",(NSLayoutFormatOptions)0,"tableview",TableView));


			SearchController = new UISearchController (searchResultsController: null);
			SearchController.Delegate = new SearchControllerDelegate ();
			SearchController.HidesNavigationBarDuringPresentation = false;
			SearchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
			SearchController.SearchBar.Placeholder = "To";
			NavigationItem.TitleView = SearchController.SearchBar;
			DefinesPresentationContext = true;
			SearchController.DimsBackgroundDuringPresentation = false;
			SearchController.DefinesPresentationContext = true;
			SearchController.SearchBar.BecomeFirstResponder ();
		}
			

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);


			ResignFirstResponder ();
//			SearchController.Editing = true;
//
			SearchController.SearchBar.BecomeFirstResponder ();
		}
			

		private  class SearchControllerDelegate : UISearchControllerDelegate{
			public override void DidPresentSearchController (UISearchController searchController)
			{
				var g  = searchController.SearchBar.CanBecomeFirstResponder;
				searchController.BecomeFirstResponder ();
				searchController.SearchBar.BecomeFirstResponder ();
			}

			public override void WillPresentSearchController (UISearchController searchController)
			{
				var g  = searchController.SearchBar.CanBecomeFirstResponder;
				int h = 0;
			}
		}


	}
}

