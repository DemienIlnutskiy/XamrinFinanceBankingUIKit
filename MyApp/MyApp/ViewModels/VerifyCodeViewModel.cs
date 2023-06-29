using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	public class VerifyCodeViewModel:BaseViewModel
	{
		#region Fields

		private string _MessageRecipient;

		private string _Recipient;

		private bool _SendingSMS;

		private string _Timer;

		#endregion Fields

		#region Properties

		public string MessageRecipient
		{ 
			get => _MessageRecipient;
			set => SetProperty(ref _MessageRecipient, value);
		}

		public string Recipient
		{
			get => _Recipient;
			set => SetProperty(ref _Recipient, value);
		}

		public bool SendingSMS
		{
			get => _SendingSMS;
			set => SetProperty(ref _SendingSMS, value);
		}

		public string Timer
		{
			get => _Timer;
			set => SetProperty(ref _Timer, value);
		}

		#endregion Properties

		#region Static Properties

		public static bool IsEmail { get; set; }
		public static bool isTimer { get; set; }
		public static int NumberTimer { get; set; }

		#endregion

		#region Commands

		public ICommand ResendMessageCommand { get; }
		public ICommand PastPageCommand { get; }

		#endregion Commands

		#region Constructors

		public VerifyCodeViewModel()
        {
			if (IsEmail)
			{
				MessageRecipient = "Check your email, we have sent you the code at ";
				Recipient = ForgotPasswordViewModel.UserEmail;
			}
			else
			{
				MessageRecipient = "Check your SMS inbox, we have sent you the code at ";
				Recipient = RegisterPhoneNumberViewModel.FlPhoneNumber;
			}

			ResendMessageCommand = new Command(async() =>await CheckSendSMS());

			PastPageCommand = new Command(async () => await RegisterPhoneNumberPage());
		}

		#endregion Constructors

		#region Private Methods

		private async Task CheckSendSMS()
		{
			bool BSendSMS;
			if (!isTimer)
			{
				RegisterPhoneNumberViewModel.VerifyCode = RegisterPhoneNumberViewModel.GenerateVerifyCode();
				if (IsEmail)
					BSendSMS = await ForgotPasswordViewModel.SendEmail(ForgotPasswordViewModel.UserEmail,
						RegisterPhoneNumberViewModel.FlPhoneNumber);
				else
					BSendSMS = await RegisterPhoneNumberViewModel.SendSms(RegisterPhoneNumberViewModel.FlPhoneNumber);

				if (BSendSMS)
				{
					SendingSMS = true;
					NumberTimer = 60;
					Timer = "(01:00)";
					isTimer = true;
					Device.StartTimer(TimeSpan.FromSeconds(1), () =>
					{
						NumberTimer--;
						if (NumberTimer < 10)
							Timer = $"(00:0{NumberTimer})";
						else
							Timer = $"(00:{NumberTimer})";
						if (NumberTimer == 0)
						{

							isTimer = false;
							return false;
						}
						return true;
					});
				}
			}
			else if (!SendingSMS)
			{
				if (IsEmail)
				{
					await Application.Current.MainPage.DisplayAlert("Failed",
						$"Please, wait {VerifyCodeViewModel.NumberTimer} seconds to send email again",
						"OK");
					return;
				}
				else
				{
					await Application.Current.MainPage.DisplayAlert("Failed",
						$"Please, wait {VerifyCodeViewModel.NumberTimer} seconds to send SMS again",
						"OK");
					return;
				}
			}
		}

		public static void StartTimer()
		{
			NumberTimer = 60;
			isTimer = true;
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				NumberTimer--;
				if (NumberTimer <= 0)
				{
					isTimer = false;
					return false;
				}
				return true;
			});
		}

		private async Task RegisterPhoneNumberPage()
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
	}
}
