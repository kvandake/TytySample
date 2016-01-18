using UIKit;
using System;
using Foundation;


namespace TytySample
{


	/// <summary>
	/// Расширение для этого проекта
	/// </summary>
	public static class TouchConstant
	{

		/// <summary>
		/// Ключи ко вьюшкам
		/// </summary>
		public static class Pages {

			public const string DashboardViewKey = "DashboardViewKey";

			public const string CitiesFromViewKey = "CitiesFromViewKey";

			public const string CitiesToViewKey = "CitiesToViewKey";

			public const string ScheduleViewKey = "ScheduleViewKey";

			public const string AboutViewKey = "AboutViewKey";

			public const string InfoStationViewKey = "InfoStationViewKey";
		}

		/// <summary>
		/// Быстрая кнопка "Отмена" 
		/// </summary>
		/// <returns>The cancel.</returns>
		/// <param name="done">Done.</param>
		public static UIBarButtonItem LeftCancel(EventHandler done){
			var button = new UIBarButtonItem (UIBarButtonSystemItem.Cancel);
			button.Clicked += done;
			return button;
		}


		/// <summary>
		/// конвертер DateTime - NSDate
		/// </summary>
		/// <returns>The NS date.</returns>
		/// <param name="date">Date.</param>
		public static NSDate ToNSDate (this DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind (date, DateTimeKind.Utc);
			return (NSDate) date;
		}


		/// <summary>
		/// конвертер NSDate - DateTime
		/// </summary>
		/// <returns>The date time.</returns>
		/// <param name="date">Date.</param>
		public static DateTime ToDateTime (this NSDate date)
		{
			return (DateTime) date;
		}


	}
}

