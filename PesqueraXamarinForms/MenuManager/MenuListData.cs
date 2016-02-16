using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace PesqueraXamarinForms
{
	public class MenuListData : List<MenuItem>
	{
		public MenuListData ()
		{
			this.Add (new MenuItem () { 
				Title = "Avance pesca por zona", 
				IconSource = "icon-Small.png", 
				TargetType = typeof(Gra01ResumenTemporadaPie)
			});

			this.Add (new MenuItem () { 
				Title = "Avance pesca por región", 
				IconSource = "icon-Small.png", 
				TargetType = typeof(Gra02PescaRegionColumn)
			});

			this.Add (new MenuItem () { 
				Title = "Avance pesca por puerto", 
				IconSource = "icon-Small.png", 
				TargetType = typeof(Gra03PescaPuertoColumn)
			});

			this.Add (new MenuItem () {
				Title = "Avance pesca por planta",
				IconSource = "icon-Small.png",
				TargetType = typeof(Gra04PescaPlantaBar)
			});

			this.Add (new MenuItem () {
				Title = "Avance pesca / descargas por día",
				IconSource = "icon-Small.png",
				TargetType = typeof(Gra05PescaDiaColumnSpline)
			});

			this.Add (new MenuItem () {
				Title = "Avance pesca / descargas quincena",
				IconSource = "icon-Small.png",
				TargetType = typeof(Gra06QuincenaColumnSpline)
			});

			this.Add (new MenuItem () {
				Title = "Avance por grupos",
				IconSource = "icon-Small.png",
				TargetType = typeof(Gra07GruposMColumn)
			});

			this.Add (new MenuItem () {
				Title = "Avance por grupos en [Rango %]",
				IconSource = "icon-Small.png",
				TargetType = typeof(Gra08GruposRangoBar)
			});
		}
	}
}