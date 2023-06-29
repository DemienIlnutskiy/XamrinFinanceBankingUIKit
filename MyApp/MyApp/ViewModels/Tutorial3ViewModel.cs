using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace MyApp.ViewModels
{
	public class Tutorial3ViewModel : BaseViewModel
	{
		#region Commands

		public ICommand EndTutorialCommand { get; }

		#endregion Commands

		#region Constructors

		public Tutorial3ViewModel()
        {
			EndTutorialCommand = new Command(Welcome);
		}

		#endregion Constructors

		#region Private Methods

		private void Welcome()
		{
			Application.Current.MainPage= new WelcomePage();
		}

		#endregion Private Methods
	}
}
