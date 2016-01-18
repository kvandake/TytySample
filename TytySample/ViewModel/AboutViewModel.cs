using System;
using System.Windows.Input;
using CoCore.Base;
using Foundation;

namespace TytySample
{
	public class AboutViewModel : ViewModelBase
	{

		/// <summary>
		/// Команда для выхода из вьюшки
		/// </summary>
		/// <value>The close command.</value>
		public ICommand CloseCommand { get; set; }


		/// <summary>
		/// Текст для передачи вьюшке
		/// </summary>
		/// <value>The app info text.</value>
		public string AppInfoText { get; set;}

		public AboutViewModel ()
		{
			//название программы
			var appName = NSBundle.MainBundle.ObjectForInfoDictionary ("CFBundleName").ToString ();
			//версия программы
			var version = NSBundle.MainBundle.ObjectForInfoDictionary ("CFBundleShortVersionString").ToString ();
			AppInfoText = string.Format ("{0} {1}", appName, version);
			CloseCommand = new RelayCommand (o => {
				var intent = CreateIntent ();
				intent.GoBack ();
			});
		}
	}
}

