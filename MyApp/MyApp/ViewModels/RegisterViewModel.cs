using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		#region Fields

		private string _Username;

		private string _Email;

		private string _Password;

		private string _ConfirmPassword;

		private bool _Agree;

		private Color _AgreeColor;

		private string _WrongName;

		private string _WrongEmail;

		private string _WrongPassword;

		private string _WrongConfirmPassword;

		#endregion Fieldss

		#region Properties

		public string Username
		{
			get => _Username;
			set => SetProperty(ref _Username, value);
		}

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

		public string ConfirmPassword
		{
			get => _ConfirmPassword;
			set => SetProperty(ref _ConfirmPassword, value);
		}

		public bool Agree
		{
			get => _Agree;
			set => SetProperty(ref _Agree, value);
		}

		public Color AgreeColor
		{
			get => _AgreeColor;
			set => SetProperty(ref _AgreeColor, value);
		}

		public string WrongName
		{
			get => _WrongName;
			set => SetProperty(ref _WrongName, value);
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

		public string WrongConfirmPassword
		{
			get => _WrongConfirmPassword;
			set => SetProperty(ref _WrongConfirmPassword, value);
		}

		#endregion Properties

		#region Static Property

		public static string Name { get; private set; }

		#endregion Static Property

		#region Commands

		public ICommand RegisterCommand { get; }
		public ICommand LoginCommand { get; }

		#endregion Commands

		#region Constructors

		public RegisterViewModel()
		{
			AgreeColor = Color.FromHex("#979797");
			LoginCommand = new Command(LoginPage);
			RegisterCommand = new Command(async () => await RegisterPage());
		}

		#endregion Constructors

		#region Private Methods

		private async Task RegisterPage()
		{

			bool rightData = true, Result;

			if (IsBusy)
				return;
			try
			{
				IsBusy = true;

				if (Username == null || Username.Length < 6)
				{
					WrongName = "Name is too short";
					rightData = false;
				}
				else
				{
					Username = Username.Trim();

					foreach (var user in App.Db.GetUsers())
						if (user.Name == Username)
						{
							WrongName = "Such the user exists";
							rightData = false;
						}
						else
							if (rightData)
							WrongName = "";
				}

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
				{
					Email = Email.Trim();
					bool rightEmail = true;
					foreach (var user in App.Db.GetUsers())
						if (user.Email == Email)
						{
							WrongEmail = "The user with this email address exists";
							rightEmail = false;
						}
						else
							if (rightEmail)
								WrongEmail = "";
							else
								rightData = false;
				}

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

				if (!Agree)
				{
					AgreeColor = Color.Red;
					return;
				}

				Email = Email.Trim();
				Password = Password.Trim();
				ConfirmPassword = ConfirmPassword.Trim();

				Result = App.Db.RegisterUser(Username, Email, Password);
				if (Result)
				{
					Name = Username;
					await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPhoneNumberPage());
				}
				else
					await Application.Current.MainPage.DisplayAlert("Error", "User already exists with this credentials", "OK");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally { IsBusy = false; }
		}

		private void LoginPage()
		{
			Application.Current.MainPage =new LogInPage();
		}

		#endregion Private Methods
	}
}
