using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tutorial2Page : ContentPage
	{
		public Tutorial2Page()
		{
			InitializeComponent();
		}

		private async void SkipTutorial_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new WelcomePage());
			for (int i = 0; i < 2; i++)
				this.Navigation.RemovePage(this.Navigation.NavigationStack[0]);
		}
	}
}