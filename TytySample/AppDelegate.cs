using Foundation;
using UIKit;
using CoCore.Base;
using CoCore.iOS;

namespace TytySample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{

		/// <summary>
		///  инициализатор логики библиотеки CoCore
		/// </summary>
		BootstrapBase bootstrap;

		/// <summary>
		/// содержит логику для навигации
		/// </summary>
		NavigationService navigation;


		// class-level declarations
		public override UIWindow Window {
			get;
			set;
		}

		public override bool WillFinishLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			navigation = new NavigationService (Window);

			//сопоставление ключей и вьюшек
			navigation.Register (TouchConstant.Pages.DashboardViewKey,typeof(DashboardView));
			navigation.Register (TouchConstant.Pages.ScheduleViewKey,typeof(ScheduleView));
			navigation.Register (TouchConstant.Pages.AboutViewKey,typeof(AboutView));
			navigation.Register (TouchConstant.Pages.InfoStationViewKey,typeof(InfoStationView));

			//позволяет с помощью ключа оборачивать вьюку в UINavigationController
			navigation.ConvertView<UIViewController>(view=>{
				var navController= new UINavigationController (view);
				return navController;
			},"navController");
			//билдер для Bootstrap
			bootstrap = new TouchBootstrapBuilder (new CoiocToEasyContainer ()).WithNavigation (navigation).Build ();
			return true;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{

			//начальная вьюшка
			var navVc = new UINavigationController (new DashboardView ());
			navigation.Start (navVc);
			InitIoc ();
			return true;
		}


		/// <summary>
		/// Добавление объектов в IoC
		/// </summary>
		void InitIoc(){
			var ioc = bootstrap.IocContainer;
			ioc.Register<ScheduleService>(()=>new ScheduleService ());
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}


