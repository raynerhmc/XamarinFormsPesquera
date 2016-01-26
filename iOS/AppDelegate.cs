using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.SfChart.XForms.iOS.Renderers;
//using Foundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Xamarin.Forms;


namespace PesqueraXamarinForms.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			new SfChartRenderer ();
			Forms.Init ();
			//global::Xamarin.Forms.Forms.Init ();

			window = new UIWindow(UIScreen.MainScreen.Bounds);

			window.RootViewController = App.GetMainPage().CreateViewController();

			window.MakeKeyAndVisible();
			//LoadApplication (new App ());

			return true;
		}
	}
}

