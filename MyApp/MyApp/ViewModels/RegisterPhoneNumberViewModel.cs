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
    public class RegisterPhoneNumberViewModel : BaseViewModel
	{
		#region Fields

		private CountryModel _selectedCountry;

		private string _PhoneNumber;

		private string _WrongPhoneNumber;

		#endregion Fields

		#region Constructors

		public RegisterPhoneNumberViewModel()
		{
			SelectedCountry = CountryUtils.GetCountryModelByName("Ukraine");
			ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
			CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as CountryModel));
			NextCommand = new Command(SendSMS);
			LoginCommand = new Command(async () => await LoginCommandAsync());
		}

		#endregion Constructors

		#region Properties

		public CountryModel SelectedCountry
		{
			get => _selectedCountry;
			set => SetProperty(ref _selectedCountry, value);
		}

		public string PhoneNumber
		{
			get => _PhoneNumber;
			set => SetProperty(ref _PhoneNumber, value);
		}

		public string WrongPhoneNumber
		{
			get => _WrongPhoneNumber;
			set => SetProperty(ref _WrongPhoneNumber, value);
		}

		#endregion Properties

		#region Static Propery

		public static string FlPhoneNumber { get; private set; }

		public static string VerifyCode { get; set; }

		#endregion Static Propery

		#region Commands

		public ICommand LoginCommand { get; }
		public ICommand ShowPopupCommand { get; }
		public ICommand CountrySelectedCommand { get; }
		public ICommand NextCommand { get; }

		#endregion Commands


		#region Private Methods

		private Task ExecuteShowPopupCommand()
		{
			var popup = new ChooseCountryPopup(SelectedCountry)
			{
				CountrySelectedCommand = CountrySelectedCommand
			};
			return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
		}

		private void ExecuteCountrySelectedCommand(CountryModel country)
		{
			SelectedCountry = country;
		}

		private async void SendSMS()
		{
			if (VerifyCodeViewModel.isTimer)
			{
				await Application.Current.MainPage.DisplayAlert("Failed", 
					$"Please, wait {VerifyCodeViewModel.NumberTimer} seconds to send SMS again",
					"OK");
				return;
			}
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				if (PhoneNumber == null||PhoneNumber.Length < 13)
				{
					WrongPhoneNumber = "Phone Number is incorrect, please enter it again";
					return;
				}
				else
					WrongPhoneNumber = "";

				string FinalPN =SelectedCountry.CountryCode +" "+ PhoneNumber.Remove(0,2);

				FlPhoneNumber = FinalPN;

				FinalPN = FinalPN.Replace(" ", String.Empty);

				VerifyCode =GenerateVerifyCode();

				if (await SendSms(FinalPN))
				{
					VerifyCodeViewModel.StartTimer();
					VerifyCodeViewModel.IsEmail = false;
					await Application.Current.MainPage.Navigation.PushModalAsync(new VerifyCodePage());
				}
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}
		#endregion Private Methods

		#region Public Static Methods

		public static async Task<bool> SendSms(string recipient)
		{
			string messageText = $"Your verify code is {VerifyCode}";
			try
			{
				var message = new SmsMessage(messageText, recipient);
				await Sms.ComposeAsync(message);
				return true;
			}
			catch (FeatureNotSupportedException)
			{
				await Application.Current.MainPage.DisplayAlert("Failed", "Sms is not supported on this device.", "OK");
				return false;
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, "OK");
				return false;
			}
		}

		public static string GenerateVerifyCode ()
		{
			Random random = new Random();

			int[] Number = new int[4];
			int verifuCode,valueVerifyCode;
			bool rightCode = true;

			do
			{
				rightCode = true;
				verifuCode = random.Next(1000, 10000);

				Number[0] = verifuCode % 10;
				valueVerifyCode = (int)verifuCode / 10;
				Number[1] = valueVerifyCode % 10;
				valueVerifyCode = (int)valueVerifyCode / 10;
				Number[2] = valueVerifyCode % 10;
				valueVerifyCode = (int)valueVerifyCode / 10;
				Number[3] = valueVerifyCode % 10;

				for (int i = 0; i < 4; i++)
					for (int j = 0; j < 4; j++)
						if (Number[i] == Number[j] && i != j)
							rightCode = false;
			}
			while (!rightCode);
			return Convert.ToString(verifuCode);
		}
		private async Task LoginCommandAsync()
		{
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;
				await Application.Current.MainPage.Navigation.PushModalAsync(new LogInPage());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		#endregion Public Static Methods
	}
}