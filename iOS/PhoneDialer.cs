using Foundation;
using PesqueraXamarinForms.iOS;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace PesqueraXamarinForms.iOS
{
	public class PhoneDialer : IDialer
	{
		public bool Dial(string number)
		{
			Console.WriteLine ("number to dial: " +  number);
			return UIApplication.SharedApplication.OpenUrl (
				new NSUrl ("tel:" + number));
		}
	}
}