using MapKit;
using CoreLocation;
using UIKit;

namespace TytySample
{

	/// <summary>
	/// Расширение класса MKAnnotation, адаптированный к Point
	/// </summary>
	public class MapAnnotation : MKAnnotation
	{
		Point location;
		CLLocationCoordinate2D _coordinate;
		string title, subtitle;

		public Point Location {
			get { return location; }
			private set {
				location = value;
				if (value == null)
					return;
				_coordinate = new CLLocationCoordinate2D (value.Latitude, value.Longitude);
			}
		}


		public void SetLocation(Point location){
			UIView.Animate(1.0, () =>
				{
					WillChangeValue("coordinate");
					_coordinate = new CLLocationCoordinate2D(location.Latitude, location.Longitude);
					DidChangeValue("coordinate");
				});
		}

		public override string Title { get{ return title; }}
		public override string Subtitle { get{ return subtitle; }}


		#region implemented abstract members of MKAnnotation

		public override CLLocationCoordinate2D Coordinate {
			get {
				return _coordinate;
			}
		}

		#endregion

		public MapAnnotation (Point location,string name, string details = null)
		{
			Location = location;
			title = name;
			subtitle = details;
		}
	}
}

