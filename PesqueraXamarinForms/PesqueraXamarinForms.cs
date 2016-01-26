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
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}