using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PesqueraXamarinForms
{
	public class GraFather : ContentPage, ILoadingGui
	{
		protected RootPage rootpage_;
		public GraFather ()
		{
			
		}
		public void  SetRootPage (RootPage ref_roofpage){
			rootpage_ = ref_roofpage;
		}

	}
}

