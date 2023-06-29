using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	public class PaymentSuccessfulViewModel:BaseViewModel
	{
		#region Commands

		public ICommand PastPageCommand { get; }
		public ICommand NextPageCommand { get; }

		#endregion Commands

		#region Constructors

		public PaymentSuccessfulViewModel()
		{
			PastPageCommand = new Command(async () => await InviteFriends());
			NextPageCommand = new Command(async () => await Reipients());
		}

		#endregion Constructors

		#region Private Methods

		private async Task InviteFriends()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				await Application.Current.MainPage.Navigation.PopModalAsync();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		private async Task Reipients()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				await Application.Current.MainPage.Navigation.PushModalAsync(new RecipientsPage());
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
