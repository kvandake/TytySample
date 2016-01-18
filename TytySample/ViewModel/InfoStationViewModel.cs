using CoCore.Base;
using System.Windows.Input;
using CoreLocation;
using MapKit;

namespace TytySample
{

	/// <summary>
	/// Логика вью модельки для отображения данных о станции. Вью - (InfoStationView)
	/// </summary>
	public class InfoStationViewModel : ViewModelBase
	{
		
		// ---------------- Теги для тегов ----------------

		public const string CellType_Default = @"Defaultcell";

		public const string CellType_ExtendedText = @"ExtendedTextcell";

		public const string CellType_Date = @"Datecell";


		// ---------------- переменные и свойства ----------------


		readonly Station station;

		readonly GroupRoot items;

		public GroupRoot Items {
			get {
				return items;
			}
		}



		/// <summary>
		/// Команда для выхода из вьюшки
		/// </summary>
		/// <value>The close command.</value>
		public ICommand CloseCommand { get; set; }


		// ---------------- методы ----------------

		public InfoStationViewModel (Station station)
		{
			Title = "Станция";
			//Инициализация данных от станции 
			//Создание списка данных и последающая их передача вьюшке
			this.station = station;
			CloseCommand = new RelayCommand (o => {
				var intent = CreateIntent ();
				intent.GoBack ();
			});

			items = new GroupRoot ();
			var section1 = new GroupSection ();
			var section2 = new GroupSection ();

			//ячейка - Страна
			var countryCell = new GroupCell ();
			countryCell.CellStyle = GroupCellStyle.Custom| GroupCellStyle.RowClick;
			countryCell.PrimaryText = "Country";
			countryCell.Tag = CellType_Default;
			countryCell.SecondaryText = station.CountryTitle;
			section1.Add (countryCell);


			//ячейка - Город
			var cityCell = new GroupCell ();
			cityCell.CellStyle = GroupCellStyle.Custom| GroupCellStyle.RowClick;
			cityCell.PrimaryText = "City";
			cityCell.Tag = CellType_Default;
			cityCell.SecondaryText = station.CityTitle;
			section1.Add (cityCell);

			if (!string.IsNullOrEmpty (station.RegionTitle)) {
				//ячейка - Регион
				var regionCell = new GroupCell ();
				regionCell.CellStyle = GroupCellStyle.Custom| GroupCellStyle.RowClick;
				regionCell.PrimaryText = "Region";
				regionCell.Tag = CellType_Default;
				regionCell.SecondaryText = station.RegionTitle;
				section1.Add (regionCell);
			}

			if (!string.IsNullOrEmpty (station.StationTitle)) {
				//ячейка - Описание станции
				var titleCell = new GroupCell ();
				titleCell.CellStyle = GroupCellStyle.Custom| GroupCellStyle.RowClick;
				titleCell.PrimaryText = station.StationTitle;
				titleCell.Tag = CellType_ExtendedText;
				section1.Add (titleCell);
			}

			items.Add (section1);


			//Если есть данные о локации
			if (station.Point != null) {
				//ячейка - локация
				var pointCell = new GroupCell ();
				pointCell.CellStyle = GroupCellStyle.Custom | GroupCellStyle.RowClick;
				pointCell.PrimaryText = "Point";
				pointCell.Tag = CellType_Date;
				pointCell.Data = station;
				pointCell.Command = new RelayCommand (o => {
					OpenMapForPlace();
				});
				section2.Add (pointCell);
				items.Add (section2);
			}
		}


		/// <summary>
		/// Открытие станции в приложении "Карты" от iOS
		/// </summary>
		public void OpenMapForPlace ()
		{
			const int regionDistance = 10000;
			var point = station.Point;
			var coord = new CLLocationCoordinate2D (point.Latitude, point.Longitude);
			var region = MKCoordinateRegion.FromDistance (coord, regionDistance, regionDistance);
			MKPlacemarkAddress address = null;
			var placemark = new MKPlacemark(coord,address);
			var mapItem = new MKMapItem (placemark);
			var option = new MKLaunchOptions ();
			option.MapCenter = region.Center;
			option.MapSpan = region.Span;
			mapItem.Name =station.StationTitle;
			mapItem.OpenInMaps (option);
		}
	}
}

