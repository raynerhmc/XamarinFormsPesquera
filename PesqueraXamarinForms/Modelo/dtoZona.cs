using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PesqueraXamarinForms
{
	public class dtoZona : INotifyPropertyChanged
	{
		string zona_name_;
		string zona_id_;

		public string codigoZona { 
			get{ return zona_id_; }
			set {
				zona_id_ = value;
				NotifyPropertyChanged ();
			}
		}

		public string descripcionZona {
			get { return zona_name_; }
			set {
				zona_name_ = value;
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

