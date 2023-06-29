using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyApp.CustomElements;
using MyApp.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VerifyCodeCustomEntry2), typeof(VerifyCodeCustomEntry2Renderer))]
namespace MyApp.Droid.Renderers
{
	public class VerifyCodeCustomEntry2Renderer : EntryRenderer
	{
		public static Drawable value;

		public VerifyCodeCustomEntry2Renderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control == null || Element == null || e.OldElement != null) return;

			var element = (VerifyCodeCustomEntry2)Element;
			var ourCustomColorHere = element.BorderColor.ToAndroid();
#pragma warning disable CS0618 // Type or member is obsolete
			Control.Background.SetColorFilter(ourCustomColorHere, PorterDuff.Mode.SrcAtop);
#pragma warning restore CS0618 // Type or member is obsolete
			value = Control.Background;
		}
	}
}