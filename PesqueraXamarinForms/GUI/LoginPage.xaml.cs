using System;
using System.Collections.Generic;

using Xamarin.Forms;
namespace PesqueraXamarinForms
{
	public partial class LoginPage : ContentPage 
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}
		void OnSignIn( object sender, EventArgs e ){
			if (String.IsNullOrEmpty(eUserName.Text) || String.IsNullOrEmpty(eUserPassword.Text) ||
				(IsValidUsernameAndPassword() == false) )
			{
				DisplayAlert("Error de validación", "Usuário o contraseña invalidas", "Volver a intentar");
			} else {
				// REMEMBER LOGIN STATUS!
				//App.Current.Properties["IsLoggedIn"] = true;
				ShowMainPage ();

				/* ILoginManager ilm = new AppPages ();
				 * ilm.ShowMainPage(); */
			}
		}
		async void ShowMainPage(){
			//var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn")?(bool)App.Current.Properties ["IsLoggedIn"]:false;

			//CrossPieCharts.FormsPlugin.Abstractions.CrossPieChartSample pieChart = new CrossPieCharts.FormsPlugin.Abstractions.CrossPieChartSample ();
			//await Navigation.PushAsync( new Gra01ResumenTemporadaPie());

			await Navigation.PushModalAsync( new RootPage());
			//await Navigation.PushAsync (pieChart.GetPageWithPieChart ());
		}

		//Function to autentificate username and password, it may change accodingly
		bool IsValidUsernameAndPassword(){
			return true;
			if ( eUserName.Text == "funcionario" && eUserPassword.Text == "123abc" ){
				return true;
			}
			return false;
		}
	}
}

