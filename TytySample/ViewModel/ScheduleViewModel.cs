using System;
using CoCore.Base;
using System.Windows.Input;
using System.Collections.Generic;
using UIKit;

namespace TytySample
{


	/// <summary>
	/// Вью модель для инициализации начальных данных и определении логики вьюшки (ScheduleView) 
	/// </summary>
	public class ScheduleViewModel : ViewModelBase, IReturnWithResult
	{

		// ---------------- Теги для тегов ----------------

		public const string CellType_FromSchedule = @"FromSchedulecell";

		public const string CellType_ToSchedule = @"ToSchedulecell";

		public const string CellType_DateSchedule = "DateSchedulecell";

		public const string CellType_Switch = "CSwitchcell";

		public const string PresentDateFormat = "dd MMMM yyyy";

		public event EventHandler<object> ShowSelectDateHandler;


		// ---------------- переменные и свойства ----------------

		readonly GroupRoot items;
		readonly GroupSection section1;
		readonly GroupCell fromCell;
		readonly GroupCell toCell;
		readonly GroupCell dateCell;
		readonly GroupCell switchCell;

		public GroupRoot Items {
			get {
				return items;
			}
		}

		/// <summary>
		/// Геттер, который упрощает доступ к сервису
		/// </summary>
		/// <value>The schedule service.</value>
		ScheduleService ScheduleService{
			get{
				return Resolve<ScheduleService> ();
			}
		}


		/// <summary>
		/// Данные для биндинга - Откуда
		/// </summary>
		string fromSchedule;
		public string FromSchedule {
			get {
				return fromSchedule;
			}
			set {
				fromSchedule = value;
				fromCell.PrimaryText = value;
				section1.Update (fromCell);
			}
		}


		/// <summary>
		/// данные для биндинга - Куда
		/// </summary>
		string toSchedule;
		public string ToSchedule {
			get {
				return toSchedule;
			}
			set {
				toSchedule = value;
				toCell.PrimaryText = value;
				section1.Update (toCell);
			}
		}


		/// <summary>
		/// Данные для биндинга - Дата
		/// </summary>
		DateTime dateSchedule;
		public DateTime DateSchedule {
			get {
				return dateSchedule;
			}
			set {
				dateSchedule = value;
				dateCell.SecondaryText = DateScheduleToString;
				section1.Update (dateCell);
			}
		}



		/// <summary>
		/// Дата в текст
		/// </summary>
		/// <value>The date schedule to string.</value>
		string DateScheduleToString {
			get {
				return dateSchedule.ToString (PresentDateFormat);
			}
		}

		/// <summary>
		/// Команда перехода во вьюшку - Откуда
		/// </summary>
		/// <value>From schedule command.</value>
		public ICommand FromScheduleCommand { get; set;}


		/// <summary>
		/// Команда для перехода во вьюшку - Куда
		/// </summary>
		/// <value>To schedule command.</value>
		public ICommand ToScheduleCommand { get; set;}


		/// <summary>
		/// Команда для выхода из вьюшки
		/// </summary>
		/// <value>The close command.</value>
		public ICommand CloseCommand { get; set; }


		// ---------------- методы ----------------


		public ScheduleViewModel ()
		{
			//Инициализация данных 
			Title = "Расписание";
			dateSchedule = DateTime.Now;
			CloseCommand = new RelayCommand (o => {
				var intent = CreateIntent ();
				intent.GoBack ();
			});
			FromScheduleCommand = new RelayCommand (o => {
				var vc = o as UIViewController;
				if(vc!=null){
					var fromSearchList = FromStations ();
					ShowSearchSearchView("from", vc,fromSearchList);
				}
			});
			ToScheduleCommand = new RelayCommand (o => {
				var vc = o as UIViewController;
				if(vc!=null){
					var toSearchList = ToStations ();
					ShowSearchSearchView("to", vc,toSearchList);
				}
			});

			items = new GroupRoot ();
			section1 = new GroupSection ();

			fromCell = new GroupCell ();
			fromCell.Tag = CellType_FromSchedule;
			fromCell.CellStyle = GroupCellStyle.Custom | GroupCellStyle.RowClick;
			fromCell.SecondaryText="From";

			toCell = new GroupCell ();
			toCell.Tag = CellType_ToSchedule;
			toCell.CellStyle = GroupCellStyle.Custom | GroupCellStyle.RowClick;
			toCell.SecondaryText="To";

			dateCell = new GroupCell ();
			dateCell.Tag = CellType_DateSchedule;
			dateCell.CellStyle = GroupCellStyle.Custom | GroupCellStyle.RowClick;
			dateCell.PrimaryText="Date";
			dateCell.SecondaryText = DateScheduleToString;
			dateCell.Command = new RelayCommand (o => {
				OnShowSelectDateHandler(o);
			});

			switchCell = new GroupCell ();
			switchCell.CellStyle = GroupCellStyle.Custom | GroupCellStyle.RowClick;
			switchCell.PrimaryText = "Отображать все станции";
			switchCell.Tag = CellType_Switch;
			switchCell.Data = true;


			section1.Add (fromCell);
			section1.Add (toCell);
			section1.Add (dateCell);
			section1.Add (switchCell);
			items.Add (section1);
		}

		public IList<SectionStation> FromStations(string id = null){
			return ScheduleService.FromStations (id);
		}

		public IList<SectionStation> ToStations(string id = null){
			return ScheduleService.ToStations (id);
		}
			

		/// <summary>
		/// Отображение SearchViewController с таблицей
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="senderVc">Sender vc.</param>
		/// <param name="searchStations">Search stations.</param>
		void ShowSearchSearchView (string context, UIViewController senderVc, IList<SectionStation> searchStations){
			var tableTest = new CitiesScheduleView (showAll,context,searchStations);
			tableTest.TableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
			var searchController = new UISearchController (tableTest);
			searchController.DimsBackgroundDuringPresentation = true;
			searchController.ObscuresBackgroundDuringPresentation = true;
			searchController.DefinesPresentationContext = false;
			senderVc.DefinesPresentationContext = true;
			searchController.SearchResultsUpdater = tableTest;
			senderVc.PresentViewController (searchController, true, null);
		}

		protected virtual void OnShowSelectDateHandler (object e)
		{
			var handler = ShowSelectDateHandler;
			if (handler != null)
				handler (this, e);
		}


		/// <summary>
		/// На выбор - показывать все станции или изначально ничего
		/// </summary>
		bool showAll = true;
		public void UpdateShowAll (bool e)
		{
			showAll = e;
			switchCell.Data = e;
		}
			

		#region IReturnWithResult implementation

		/// <summary>
		/// этот метод вызывается после того, как будет вызван intent.GoBackWithResult() от дочерней вьюшки -- ( реализация от собственной либы CoCore)
		/// </summary>
		/// <returns><c>true</c>, if with result was returned, <c>false</c> otherwise.</returns>
		/// <param name="intent">Intent.</param>
		public bool ReturnWithResult (Intent intent)
		{
			var context = intent.Get<string> ("context");
			var station = intent.Get<Station> ("station");
			if (context == "to") {
				ToSchedule = station.StationTitle;
			} else if (context == "from") {
				FromSchedule = station.StationTitle;
			}
			return true;
		}

		#endregion
	}
}

