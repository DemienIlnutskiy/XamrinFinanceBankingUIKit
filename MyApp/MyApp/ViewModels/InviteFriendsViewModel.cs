using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	public class InviteFriendsViewModel:BaseViewModel
	{
		#region Commands

		public ICommand PastPageCommand { get; }
		public ICommand NextPageCommand { get; }

		#endregion Commands

		#region Constructors

		public InviteFriendsViewModel()
		{
			PastPageCommand = new Command(LogInPage);

			NextPageCommand = new Command(async() => await PaymentSuccessful());
		}

		#endregion Constructors

		#region Private Methods

		private void LogInPage()
		{
			Application.Current.MainPage = new LogInPage();
		}

		private async Task PaymentSuccessful()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				await Application.Current.MainPage.Navigation.PushModalAsync(new PaymentSuccessfulPage());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		#endregion Private Methods
	}
}
