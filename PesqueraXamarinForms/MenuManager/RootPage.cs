﻿using System;
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
			Detail = new NavigationPage (new Gra01ResumenTemporadaPie ());
		}

		void NavigateTo (MenuItem menu)
		{
			if (menu == null)
				return;

			Page displayPage = (Page)Activator.CreateInstance (menu.TargetType);

			Detail = new NavigationPage (displayPage);

			menuPage.Menu.SelectedItem = null;
			IsPresented = false;
		}
	}
}