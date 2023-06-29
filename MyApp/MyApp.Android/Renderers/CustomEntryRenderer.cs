using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MyApp.Droid.Renderers;
using System.ComponentModel;
using MyApp.CustomElements;
using MyApp.Views;
using System;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace MyApp.Droid.Renderers
{
	public class CustomEntryRenderer : EntryRenderer
	{
		public CustomEntryRenderer(Context context) : base(context) { }

		public CustomEntry ElementV2 => Element as CustomEntry;

		protected override FormsEditText CreateNativeControl()
		{
			var control = base.CreateNativeControl();
			UpdateBackground(control);
			return control;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == VerifyCodeCustomEntry.CornerRadiusProperty.PropertyName)
			{
				UpdateBackground();
			}
			else if (e.PropertyName == VerifyCodeCustomEntry.BorderThicknessProperty.PropertyName)
			{
				UpdateBackground();
			}
			else if (e.PropertyName == VerifyCodeCustomEntry.BorderColorProperty.PropertyName)
			{
				UpdateBackground();
			}

			base.OnElementPropertyChanged(sender, e);
		}

		protected override void UpdateBackgroundColor()
		{
			UpdateBackground();
		}
		protected void UpdateBackground(FormsEditText control)
		{
			if (control == null) return;

			var gd = new GradientDrawable();
			gd.SetColor(Element.BackgroundColor.ToAndroid());
			gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
			gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
			control.SetBackground(gd);

			var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
			var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
			var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
			var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

			control.SetPadding(padLeft, padTop, padRight, padBottom);
		}
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
		protected void UpdateBackground()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
		{
			UpdateBackground(Control);
		}
	}
}