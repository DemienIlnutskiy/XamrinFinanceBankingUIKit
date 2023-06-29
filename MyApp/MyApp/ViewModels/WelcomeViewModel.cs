using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace MyApp.ViewModels
{
    public class WelcomeViewModel
	{
		#region Commands

		public ICommand NextPageCommand { get; }

		#endregion Commands

		#region Constructors

		public WelcomeViewModel()
        {
			NextPageCommand = new Command(Register);
        }

		#endregion Constructors

		#region Private Methods

		private void Register()
        {
            Application.Current.MainPage = new RegisterPage();
		}

		#endregion Private Methods
	}
}
