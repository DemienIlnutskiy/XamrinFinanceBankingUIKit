using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
	public class WelcomeNameViewModel : BaseViewModel
	{
		#region Fields

		private string _Greetin;

		#endregion Fields

		#region Properties

		public string Greetin
		{
			get => _Greetin;
			set => SetProperty(ref _Greetin, value);
		}

		#endregion Properties

		#region Commands

		public ICommand NextPageCommand { get; }

		#endregion Commands

		#region Constructors

		public WelcomeNameViewModel()
        {
			Greetin ="Welcome "+ RegisterViewModel.Name;

			NextPageCommand = new Command(InviteFriendsPage);
        }

		#endregion Constructors

		#region Private Methods

		private void InviteFriendsPage()
		{
			Application.Current.MainPage = new InviteFriendsPage();
		}

		#endregion Private Methods
	}
}
