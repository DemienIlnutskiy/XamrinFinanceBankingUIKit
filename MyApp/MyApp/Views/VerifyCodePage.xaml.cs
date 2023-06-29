using MyApp.CustomElements;
using MyApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerifyCodePage : ContentPage
	{
		public static bool EntryHasText { get; private set; }
		public static bool BFirstNumber { get; private set; }

		public VerifyCodePage()
		{
			InitializeComponent();
		}

		private void Number_TextChanged(object sender, TextChangedEventArgs e)
		{
			CodeTextChanged(sender);
		}

		private void Number_OnBackspace(object sender, EventArgs e)
		{
			ClickBackspace(sender);
		}

		#region My Private Methods

		private async void CodeTextChanged(object sender)
		{
			VerifyCodeCustomEntry entry = sender as VerifyCodeCustomEntry;

			EntryHasText = true;

			if (FourthNumber.Text != null && FourthNumber.Text.Length >= 1)
			{
				string VerifyCode = FirstNumber.Text + SecondNumber.Text + ThirdNumber.Text + FourthNumber.Text;
				if (RegisterPhoneNumberViewModel.VerifyCode != VerifyCode)
					await DisplayAlert("Failed", "Verify Code is wrong", "OK");
				else
				{
					if (VerifyCodeViewModel.IsEmail)
						Application.Current.MainPage = new CreateNewPasswordPage();
					else
					{
						App.Db.InsertPhoneNumber(RegisterPhoneNumberViewModel.FlPhoneNumber);
						Application.Current.MainPage = new WelcomeNamePage();
					}
				}
				return;
			}

			if (FirstNumber.Text != null && FirstNumber.Text.Length == 1 && !FirstNumber.IsReadOnly)
			{
				BFirstNumber = true;
				FirstNumber.IsReadOnly = true;
				SecondNumber.IsReadOnly = false;
				SecondNumber.Focus();
			}
			if (SecondNumber.Text != null && SecondNumber.Text.Length == 1 && !SecondNumber.IsReadOnly)
			{
				SecondNumber.IsReadOnly = true;
				ThirdNumber.IsReadOnly = false;
				ThirdNumber.Focus();
			}
			if (ThirdNumber.Text != null && ThirdNumber.Text.Length == 1 && !ThirdNumber.IsReadOnly)
			{
				ThirdNumber.IsReadOnly = true;
				FourthNumber.IsReadOnly = false;
				FourthNumber.Focus();
			}

			EntryHasText = false;
			BFirstNumber = false;
		}

		private void ClickBackspace (object sender)
		{
			if ((SecondNumber.Text == null || SecondNumber.Text.Length == 0) && !SecondNumber.IsReadOnly)
			{
				EntryHasText = false;
				SecondNumber.IsReadOnly = true;
				EntryHasText = true;
				BFirstNumber = true;
				FirstNumber.Focus();
				FirstNumber.IsReadOnly = false;
			}
			if ((ThirdNumber.Text == null || ThirdNumber.Text.Length == 0) && !ThirdNumber.IsReadOnly)
			{
				EntryHasText = false;
				ThirdNumber.IsReadOnly = true;
				EntryHasText = true;
				SecondNumber.Focus();
				SecondNumber.IsReadOnly = false;
			}
			if ((FourthNumber.Text == null || FourthNumber.Text.Length == 0) && !FourthNumber.IsReadOnly)
			{
				EntryHasText = false;
				FourthNumber.IsReadOnly = true;
				EntryHasText = true;
				ThirdNumber.Focus();
				ThirdNumber.IsReadOnly = false;
			}
		}

		#endregion My Private Methods
	}
}