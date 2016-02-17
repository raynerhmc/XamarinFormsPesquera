using System;
using Xamarin.Forms;
using System.Linq;

namespace PesqueraXamarinForms
{
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage;

		public RootPage ()
		{
			menuPage = new MenuPage ();

			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as MenuItem);

			Master = menuPage;
			GraFather displayPage = new Gra01ResumenTemporadaPie ();
			displayPage.SetRootPage (this);
			Detail = new NavigationPage (displayPage);

		}

		void NavigateTo (MenuItem menu)
		{
			if (menu == null)
				return;

			GraFather displayPage = (GraFather)Activator.CreateInstance (menu.TargetType);
			displayPage.SetRootPage (this);

			Detail = new NavigationPage (displayPage);

			menuPage.Menu.SelectedItem = null;
			IsPresented = false;
		}

		public async void ShowGra02PescaRegionColumnWithData(HttpJsonLoader http_loader, int anio_index , 
			int zona_index , int periodo_index){

			GraFather displayPage = new Gra02PescaRegionColumn (http_loader, anio_index, 
				                        zona_index, periodo_index);
			displayPage.SetRootPage (this);
			await Detail.Navigation.PushAsync (new NavigationPage (displayPage));
			//Detail = new NavigationPage (displayPage);

		}

		public async void ShowGra03PescaPuertoColumnWithData( HttpJsonLoader http_loader, int anio_index, int zona_index, int periodo_index, int region_index ){
			
			GraFather displayPage = new Gra03PescaPuertoColumn (http_loader, anio_index, 
				                        zona_index, periodo_index, region_index);
			displayPage.SetRootPage (this);
			await Detail.Navigation.PushAsync (new NavigationPage (displayPage));
			//Detail = new NavigationPage (displayPage);
		}

		public async void ShowGra04PescaPlantaBarWithData( HttpJsonLoader http_loader, int anio_index, int zona_index, 
			int periodo_index, int region_index, int puerto_index ){

			GraFather displayPage = new Gra04PescaPlantaBar (http_loader, anio_index, 
				                        zona_index, periodo_index, region_index, puerto_index);
			displayPage.SetRootPage (this);
			await Detail.Navigation.PushAsync (new NavigationPage (displayPage));
			//Detail = new NavigationPage (displayPage);
		}

		public async void ShowGra08GruposRangoBarWithData( HttpJsonLoader http_loader, int anio_index, int zona_index, 
			int periodo_index, int rango_index ){

			GraFather displayPage = new Gra08GruposRangoBar (http_loader, anio_index, 
				                        zona_index, periodo_index, rango_index);
			displayPage.SetRootPage (this);
			await Detail.Navigation.PushAsync (new NavigationPage (displayPage));
			//Detail = new NavigationPage (displayPage);
		}
	}
}