using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace MyApp.ViewModels
{
	public class StartViewModel: BaseViewModel
	{
		#region Commands

		public ICommand NextPageCommand { get; }

		#endregion Commands

		#region Constructors

		public StartViewModel()
        {
			NextPageCommand = new Command(Tutorial1);
        }

		#endregion Constructors

		#region Private Methods

		private void Tutorial1()
		{
			Application.Current.MainPage = new Tutorial1Page();
		}

		#endregion Private Methods
	}
}
