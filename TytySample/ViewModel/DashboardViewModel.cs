using CoCore.Base;
using System.Windows.Input;

namespace TytySample
{

	/// <summary>
	/// Вью модель для определении логики навигации в приложении
	/// </summary>
	public class DashboardViewModel : ViewModelBase
	{

		/// <summary>
		/// Команда для перехода во вьюшку "Расписание"
		/// </summary>
		/// <value>The schedule command.</value>
		public ICommand ScheduleCommand {get;set;}

		/// <summary>
		/// Команда для перехода во вьюшку "О программе"
		/// </summary>
		/// <value>The about command.</value>
		public ICommand AboutCommand { get; set;}

		public DashboardViewModel ()
		{
			Title= "Панелька";
			ScheduleCommand = new RelayCommand (o => {
				var intent = CreateIntent ();
				//позволяет передавать в навигацию "теги", которые инициализируются в NavigationService(инициализирован в AppDelegate). Таким образом абстрагируемся от конкретной платформы
				//В данном случае "presenting" позволяет вьюшку отобразить как PresentViewController
				//"navController" - оборачиваем вьюшку в UINavigationController
				intent.Navigate (TouchConstant.Pages.ScheduleViewKey,"presenting","navController");
			});
			AboutCommand = new RelayCommand (o => {
				var intent = CreateIntent ();
				//В данном случае "presenting" позволяет вьюшку отобразить как PresentViewController
				intent.Navigate (TouchConstant.Pages.AboutViewKey,"presenting");
			});
		}
	}
}

