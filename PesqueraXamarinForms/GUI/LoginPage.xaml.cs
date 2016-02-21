using System;
using System.Collections.Generic;

using Xamarin.Forms;
namespace PesqueraXamarinForms
{
	public partial class LoginPage : ContentPage 
	{
		int num_of_tries = GlobalParameters.LOGIN_NUMBER_OF_TRIES;

		public LoginPage ()
		{
			InitializeComponent ();
		}

		protected override void OnPropertyChanged (string propertyName)
		{
			base.OnPropertyChanged (propertyName);
			if (Application.Current == null)
				return;
			if (Application.Current.Properties.ContainsKey (GlobalParameters.LOGIN_USERNAME_KEY) 
				&& Application.Current.Properties.ContainsKey (GlobalParameters.LOGIN_PASSWORD_KEY)
				&& Application.Current.Properties.ContainsKey (GlobalParameters.LOGIN_SAVELOGIN_KEY)) {
				eUserName.Text = Application.Current.Properties [GlobalParameters.LOGIN_USERNAME_KEY].ToString ();
				eUserPassword.Text = Application.Current.Properties [GlobalParameters.LOGIN_PASSWORD_KEY].ToString ();
				sSaveLogin.IsToggled = Boolean.Parse (Application.Current.Properties [GlobalParameters.LOGIN_SAVELOGIN_KEY].ToString ());
			}
		}
			
		void OnSignIn( object sender, EventArgs e ){
			if (String.IsNullOrEmpty(eUserName.Text) || String.IsNullOrEmpty(eUserPassword.Text) ||
				(IsValidUsernameAndPassword() == false) )
			{
				num_of_tries--;
				if (num_of_tries == 0) {
					DisplayAlert ("Error de validación", 
						"Usuário o contraseña invalidas \n Número de intentos restantes: 0", 
						"Vuelva a intentar en otro momento");
					bEnterLogin.IsEnabled = false;
				} else {
					DisplayAlert ("Error de validación", 
						"Usuário o contraseña invalidas \n Número de intentos restantes: " + num_of_tries, 
						"Volver a intentar");
				}
			} else {
				// REMEMBER LOGIN STATUS!
				//App.Current.Properties["IsLoggedIn"] = true;
				if (sSaveLogin.IsToggled) {
					Application.Current.Properties [GlobalParameters.LOGIN_USERNAME_KEY] = eUserName.Text as string;
					Application.Current.Properties [GlobalParameters.LOGIN_PASSWORD_KEY] = eUserPassword.Text as string;
					Application.Current.Properties [GlobalParameters.LOGIN_SAVELOGIN_KEY] = sSaveLogin.IsToggled.ToString();

				} else {
					Application.Current.Properties [GlobalParameters.LOGIN_USERNAME_KEY] = "" as string;
					Application.Current.Properties [GlobalParameters.LOGIN_PASSWORD_KEY] = "" as string;
					Application.Current.Properties [GlobalParameters.LOGIN_SAVELOGIN_KEY] = sSaveLogin.IsToggled.ToString();
				}
				Application.Current.SavePropertiesAsync ();
				ShowMainPage ();
			}
		}

		async void ShowMainPage(){
			await Navigation.PushModalAsync( new RootPage());
		}

		//Function to autentificate username and password, it may change accodingly
		bool IsValidUsernameAndPassword(){
			if ( eUserName.Text == "funcionario" && eUserPassword.Text == "123abc" ){
				return true;
			}
			return false;
		}
	}
}

