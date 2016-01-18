using System;

using Foundation;
using UIKit;
using MapKit;

namespace TytySample
{

	/// <summary>
	/// ячейка с картой
	/// </summary>
	public partial class MapViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("MapViewCell");
		public static readonly UINib Nib;
		MapAnnotation annotation;
		static readonly int DefaultZoom = 10000;

		static MapViewCell ()
		{
			Nib = UINib.FromName ("MapViewCell", NSBundle.MainBundle);
		}

		public static MapViewCell Create ()
		{
			return (MapViewCell)Nib.Instantiate (null, null) [0];
		}

		public MapViewCell (IntPtr handle) : base (handle)
		{
		}

		public void Config(){
			MapView.Layer.CornerRadius = 10;
			MapView.Layer.MasksToBounds = true;
			MapView.UserInteractionEnabled = false;
		}

		public void UpdateLocation(Point location,string name, string details){
			if (annotation != null) {
				MapView.RemoveAnnotation (annotation);
			}
			annotation = new MapAnnotation (location,name,details);
			MapView.CenterCoordinate = annotation.Coordinate;
			MapView.SetRegion(MKCoordinateRegion.FromDistance(annotation.Coordinate,DefaultZoom,DefaultZoom), true);
			MapView.AddAnnotation (annotation);
		}
	}
}
