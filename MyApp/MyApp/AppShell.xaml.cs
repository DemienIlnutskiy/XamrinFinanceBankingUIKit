using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyApp
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			App.Current.MainPage = new AppShell();
		}
	}
}
