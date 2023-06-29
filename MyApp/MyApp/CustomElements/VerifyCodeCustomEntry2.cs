using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.CustomElements
{
	public class VerifyCodeCustomEntry2 : Entry
	{
		public static readonly BindableProperty BorderColorProperty = 
			BindableProperty.Create("BorderColor", typeof(Color), typeof(Entry), Color.White);

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}
	}
}
