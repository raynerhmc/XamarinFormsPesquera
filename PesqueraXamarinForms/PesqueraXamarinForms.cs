using System;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage(new PesqueraXamarinForms.LoginPage ());
		}
		public static Page GetMainPage(){
			return new NavigationPage(new PesqueraXamarinForms.LoginPage ());
		}
		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnSleep ()
		{
			base.OnSleep ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
		}
	}
}