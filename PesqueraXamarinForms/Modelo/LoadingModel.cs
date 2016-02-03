using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PesqueraXamarinForms
{
	public class LoadingModel : INotifyPropertyChanged
	{
		private bool isLoading;
		public bool IsLoading
		{
			get
			{
				return this.isLoading;
			}

			set
			{
				this.isLoading = value;
				NotifyPropertyChanged();
			}
		}

		public LoadingModel()
		{
			IsLoading = false;
		}

		//the view will register to this event when the DataContext is set
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	} 
}

