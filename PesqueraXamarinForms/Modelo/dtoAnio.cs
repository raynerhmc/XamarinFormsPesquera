using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace PesqueraXamarinForms
{
	public class dtoAnio : INotifyPropertyChanged
	{
		int anho;
		public int anoTempo { get { return anho; }
			set {
				anho = value;
				NotifyPropertyChanged();
			} }

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

