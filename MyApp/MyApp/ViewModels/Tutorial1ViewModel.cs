using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace MyApp.ViewModels
{
	public class Tutorial1ViewModel: BaseViewModel
	{
		#region Commands

		public ICommand NextPageCommand { get; }
		public ICommand SkipTutorialCommand { get; }

		#endregion Commands

		#region Constructors

		public Tutorial1ViewModel()
        {
			NextPageCommand = new Command(async()=>await Tutorial2());
			SkipTutorialCommand = new Command(Welcome);
		}

		#endregion Constructors

		#region Private Methods

		private async Task Tutorial2()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				await Application.Current.MainPage.Navigation.PushModalAsync(new Tutorial2Page());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		private void Welcome()
		{
			Application.Current.MainPage=new WelcomePage();
		}

		#endregion Private Methods
	}
}
