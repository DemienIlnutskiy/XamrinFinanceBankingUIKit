using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	internal class CreateNewPasswordViewModel:BaseViewModel
	{
		#region Fields

		private string _Password;

		private string _WrongPassword;

		private string _ConfirmPassword;

		private string _WrongConfirmPassword;

		#endregion Fieldss

		#region Properties

		public string Password
		{
			get => _Password;
			set => SetProperty(ref _Password, value);
		}

		public string WrongPassword
		{
			get => _WrongPassword;
			set => SetProperty(ref _WrongPassword, value);
		}

		public string ConfirmPassword
		{
			get => _ConfirmPassword;
			set => SetProperty(ref _ConfirmPassword, value);
		}

		public string WrongConfirmPassword
		{
			get => _WrongConfirmPassword;
			set => SetProperty(ref _WrongConfirmPassword, value);
		}

		#endregion Properties

		#region Commands

		public ICommand CreatePasswordCommand { get; }
		public ICommand PastPageCommand { get; }

		#endregion Commands

		#region Constructors

		public CreateNewPasswordViewModel()
		{
			CreatePasswordCommand = new Command(async () => await ChangePassword());
			PastPageCommand = new Command(async () => await LogInPage());
		}

		#endregion Constructors

		#region Private Methods

		private async Task ChangePassword()
		{
			bool Result, rightData = true;
			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				if (Password == null || Password.Length < 8)
				{
					WrongPassword = "Password is too short";
					rightData = false;
				}
				else
					WrongPassword = "";

				if (ConfirmPassword == null || ConfirmPassword != Password)
				{
					WrongConfirmPassword = "Confirm password is wrong";
					rightData = false;
				}
				else
					WrongConfirmPassword = "";

				if (!rightData)
					return;

				Result = App.Db.UpdatePasword(Password,ForgotPasswordViewModel.UserEmail);

				if (Result)
				{
					Application.Current.MainPage = new LogInPage();
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
	}
}
