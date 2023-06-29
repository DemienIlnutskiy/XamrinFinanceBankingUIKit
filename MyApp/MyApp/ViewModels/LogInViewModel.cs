using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SQLite.SQLite3;

namespace MyApp.ViewModels
{
	public class LogInViewModel : BaseViewModel
	{
		#region Fields

		private string _Email;

		private string _Password;

		private string _WrongEmail;

		private string _WrongPassword;

		#endregion Fieldss

		#region Properties


		public string Email
		{
			get => _Email;
			set => SetProperty(ref _Email, value);
		}

		public string Password
		{
			get => _Password;
			set => SetProperty(ref _Password, value);
		}

		public string WrongEmail
		{
			get => _WrongEmail;
			set => SetProperty(ref _WrongEmail, value);
		}

		public string WrongPassword
		{
			get => _WrongPassword;
			set => SetProperty(ref _WrongPassword, value);
		}

		#endregion Properties

		#region Commands

		public ICommand RegisterCommand { get; }
		public ICommand LoginCommand { get; }
		public ICommand ForgotPasswordCommand { get; }

		#endregion Commands

		#region Constructors

		public LogInViewModel()
		{
			LoginCommand = new Command(async () => await LoginPage());
			RegisterCommand = new Command(RegisterPage);
			ForgotPasswordCommand = new Command(async () => await ForgotPasswordPage());
		}

		#endregion Constructors

		#region Private Methods

		private void RegisterPage()
		{
			Application.Current.MainPage = new RegisterPage();
		}

		private async Task LoginPage()
		{
			bool rightData = true, Result;
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				if (Email == null || Email.Length < 6)
				{
					WrongEmail = "Email address is too short";
					rightData = false;
				}
				else
					if (Email.IndexOf("@") == -1 || Email.IndexOf(".") == -1)
				{
					WrongEmail = "Special characters of the e-mail address aren't used";
					rightData = false;
				}
				else
					WrongEmail = "";

				if (Password == null || Password.Length < 8)
				{
					WrongPassword = "Password is too short";
					rightData = false;
				}
				else
					WrongPassword = "";

				if (!rightData)
					return;

				Email = Email.Trim();
				Password = Password.Trim();

				Result = App.Db.LoginUser(Email, Password);

				if (Result)
				{
					Application.Current.MainPage = new InviteFriendsPage();
				}
				else
					await Application.Current.MainPage.DisplayAlert("Error", "User doesn't exist with this credentials", "OK");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		private async Task ForgotPasswordPage()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPasswordPage());
		}

		#endregion Private Methods
	}
}
