using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Views;
using MyApp.ViewModels;
using MyApp.AppContext;
using System.IO;

namespace MyApp
{
	public partial class App : Application
	{
		private static DB db;
		public static DB Db
		{
			get
			{
				if (db == null)
					db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sqlite"));
				return db;
			}
		}

		public App()
		{
			InitializeComponent();

			MainPage= new StartPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
