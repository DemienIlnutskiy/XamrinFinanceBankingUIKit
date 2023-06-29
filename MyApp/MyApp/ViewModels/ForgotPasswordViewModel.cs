using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyApp.Models;
using MyApp.Popups;
using MyApp.Utils;
using System;
using Xamarin.Essentials;
using MyApp.Views;

namespace MyApp.ViewModels
{
	public class ForgotPasswordViewModel:BaseViewModel
	{
		#region Fields

		private string _Email;

		private string _WrongEmail;

		#endregion Fieldss

		#region Properties


		public string EmailAddress
		{
			get => _Email;
			set => SetProperty(ref _Email, value);
		}

		public string WrongEmail
		{
			get => _WrongEmail;
			set => SetProperty(ref _WrongEmail, value);
		}

		#endregion Properties

		#region Static Properties

		public static string UserEmail { get; private set; }

		#endregion Static Properties

		#region Commands

		public ICommand SendEmailCommand { get; }
		public ICommand PastPageCommand { get; }

		#endregion Commands

		#region Constructors

		public ForgotPasswordViewModel()
		{
			SendEmailCommand = new Command(async () => await Send());
			PastPageCommand = new Command(async () => await LogInPage());
		}

		#endregion Constructors

		#region Private Methods

		private async Task Send()
		{
			if (VerifyCodeViewModel.isTimer)
			{
				await Application.Current.MainPage.DisplayAlert("Failed",
					$"Please, wait {VerifyCodeViewModel.NumberTimer} seconds to send email again",
					"OK");
				return;
			}
			bool Result,rightData=true;
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				if (EmailAddress == null || EmailAddress.Length < 6)
				{
					WrongEmail = "Email address is too short";
					rightData = false;
				}
				else
					if (EmailAddress.IndexOf("@") == -1 || EmailAddress.IndexOf(".") == -1)
				{
					WrongEmail = "Special characters of the e-mail address aren't used";
					rightData = false;
				}
				else
				{
					EmailAddress = EmailAddress.Trim();

					foreach (var user in App.Db.GetUsers())
					{
						if (user.Email == EmailAddress)
						{
							rightData = true;
							break;
						}
						else
							rightData = false;
					}
					if (!rightData)
						WrongEmail = "The user with this email address doesn't exist";
					else
						WrongEmail = "";
				}

				if (!rightData)
					return;

				RegisterPhoneNumberViewModel.VerifyCode = RegisterPhoneNumberViewModel.GenerateVerifyCode();
				Result =await SendEmail(EmailAddress, RegisterPhoneNumberViewModel.VerifyCode);

				if (Result)
				{
					VerifyCodeViewModel.StartTimer();
					VerifyCodeViewModel.IsEmail = true;
					UserEmail = EmailAddress;
					await Application.Current.MainPage.Navigation.PushModalAsync(new VerifyCodePage());
				}
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		private async Task LogInPage()
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

		#endregion Private Methods

		#region Static Methods

		public static async Task<bool> SendEmail(string recipient, string VerifyCode)
		{
			string messageText = $"Your verify code is {VerifyCode}";
			try
			{
				await Email.ComposeAsync("Finance Banking UI Kit", messageText, recipient);
				return true;
			}
			catch (FeatureNotSupportedException)
			{
				await Application.Current.MainPage.DisplayAlert("Failed", "Sending emails to an email address isn`t supported on this device.", "OK");
				return false;
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, "OK");
				return false;
			}
		}

		#endregion Static Methods
	}
}
