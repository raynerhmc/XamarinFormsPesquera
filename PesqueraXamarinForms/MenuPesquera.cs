using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
	public class MenuPesquera : ContentPage
	{
		Picker reutlizable_view;
		public MenuPesquera (View content)
		{
			reutlizable_view = GetMenu ();
			this.Content = content;
		}
		public Picker GetReuMenu(){
			return reutlizable_view;
		}
		private  Picker GetMenu(){
			Picker p_list_menu = new Picker
			{
				Title = "Cuadros",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] menuNameList = {"Avance pesca por zona",
				"Avance pesca por región",
				"Avance pesca por puerto",
				"Avance pesca por planta",
				"Avance pesca / descargas por día",
				"Avance pesca / descargas quincena",
				"Avance por grupos",
				"Avance por grupos en [Rango %]",
			};
			foreach (string menuName in menuNameList)
			{
				p_list_menu.Items.Add(menuName);
			}
			p_list_menu.SelectedIndex = 0;

			// WHEN p_list_menu is selected
			p_list_menu.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_menu.SelectedIndex == -1)
				{
				}
				else
				{
					switch(p_list_menu.SelectedIndex) 
					{
					case 1:
						{
							ShowMyPage();
							break;
						}
					}

				}
			};

			return p_list_menu;
			/*
			return new StackLayout(){
				Spacing = 15,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = {
					p_list_menu
				}
			}*/
		}

		async void ShowMyPage(){
			
			await this.Content.Navigation.PushAsync( new MyPage() ) ;
		}
	}
}

