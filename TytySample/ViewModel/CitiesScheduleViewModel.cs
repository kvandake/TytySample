using System;
using CoCore.Base;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Foundation;

namespace TytySample
{
	public class CitiesScheduleViewModel : ViewModelBase
	{

		// ---------------- переменные и свойства ----------------


		/// <summary>
		/// переменная для определения того, с чем работает вью моделька: "from" или "to", т.е. выбрано откуда или куда
		/// </summary>
		readonly string _context;



		ExtendedObservableCollection<SectionStation> _stations;
		ExtendedObservableCollection<SectionStation> _filteredStations;


		/// <summary>
		/// все станции, для быстрого отображения (можно оптимайзить и обойтись без этого)
		/// </summary>
		ExtendedObservableCollection<SectionStation> Stations {
			get {
				return _stations ?? (_stations = new ExtendedObservableCollection<SectionStation> ());
			}
		}

		/// <summary>
		/// отфильтрованные станции (после воода в SearchBar данные обновляются)
		/// </summary>
		public ExtendedObservableCollection<SectionStation> FilteredStations {
			get {
				return _filteredStations ??(_filteredStations = Stations);
			}
			set {
				_filteredStations = value;
			}
		}


		/// <summary>
		/// Команда для перехода во вьюшку "Подробнее о станции"
		/// </summary>
		/// <value>The show info command.</value>
		public ICommand ShowInfoCommand { get; set;}


		/// <summary>
		/// Команда для выхода из вьюшки
		/// </summary>
		/// <value>The go back withresult command.</value>
		public ICommand GoBackWithresultCommand { get; set;}


		// ---------------- методы ----------------



		/// <summary>
		/// ОБновление фильрованного списка
		/// </summary>
		/// <param name="query">Query.</param>
		public void UpdateFilteredStations(string query){
			if (query == null)
				return;
			var filterList = Stations.Where (x => x.ToString ().IndexOf (query, StringComparison.OrdinalIgnoreCase) >= 0).ToList ();
			FilteredStations = new ExtendedObservableCollection<SectionStation> (filterList);
		}


		public CitiesScheduleViewModel (string context, IList<SectionStation> stations)
		{
			_context = context;
			Stations.AddRange (stations);
			ShowInfoCommand = new RelayCommand (o => {
				var index = o as NSIndexPath;
				var intent = CreateIntent ();
				if (index.Section < FilteredStations.Count) {
					intent.Put ("station", FilteredStations [index.Section].Stations[index.Row]);
					//В данном случае "presenting" позволяет вьюшку отобразить как PresentViewController
					//"navController" - оборачиваем вьюшку в UINavigationController
					intent.Navigate (TouchConstant.Pages.InfoStationViewKey, "presenting", "navController");
				}
			});
			GoBackWithresultCommand = new RelayCommand (o => {
				//добавляем данные для передачи их в родительскую вьюшку (у родительской вьюшки вызывается ReturnWithResult (Intent intent))
				var intent = CreateIntent ();
				intent.Put ("context",_context);
				intent.Put ("station",o);
				intent.GoBackWithResult ();
			});
		}





	}
}

