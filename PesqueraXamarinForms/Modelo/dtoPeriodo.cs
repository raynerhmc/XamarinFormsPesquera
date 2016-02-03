using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PesqueraXamarinForms
{
	public class dtoPeriodo : INotifyPropertyChanged
	{
		string periodo_;

		public string periodo { 
			get{ return periodo_; }
			set {
				periodo_ = value;
				NotifyPropertyChanged ();
			}
		}


		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}

