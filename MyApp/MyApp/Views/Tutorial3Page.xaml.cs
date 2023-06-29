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
	public partial class Tutorial3Page : ContentPage
	{
		public Tutorial3Page()
		{
			InitializeComponent();
		}

		private async void FinishTutorial_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new WelcomePage());
			for(int i = 0; i < 3; i++)  
				this.Navigation.RemovePage(this.Navigation.NavigationStack[0]);
		}
	}
}