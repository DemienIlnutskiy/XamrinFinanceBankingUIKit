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
using Android.Graphics;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(VerifyCodeCustomEntry), typeof(VerifyCodeCustomEntryRenderer))]
namespace MyApp.Droid.Renderers
{
	public class VerifyCodeCustomEntryRenderer : EntryRenderer
	{
		Drawable defaultTextBackgroundColor;
		public VerifyCodeCustomEntryRenderer(Context context) : base(context) { }

		public VerifyCodeCustomEntry ElementV2 => Element as VerifyCodeCustomEntry;
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
		public override bool DispatchKeyEvent(KeyEvent e)
		{
			if (e.Action == KeyEventActions.Down)
			{
				if (e.KeyCode == Keycode.Del)
				{
					if (string.IsNullOrWhiteSpace(Control.Text))
					{
						var entry = (VerifyCodeCustomEntry)Element;
						entry.OnBackspacePressed();
					}
				}
			}
			return base.DispatchKeyEvent(e);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

			if (Control == null) return;

			defaultTextBackgroundColor = Control.Background;
			if (defaultTextBackgroundColor != null)
				Control.Background = VerifyCodeCustomEntry2Renderer.value;

			Control.FocusChange += (sender, eh) =>
			{
				if (defaultTextBackgroundColor != null)
				{
					VerifyCodeCustomEntry entry = (VerifyCodeCustomEntry)Element;
					if (eh.HasFocus)
					{
						Control.Background = defaultTextBackgroundColor;
						entry.Text = "";
						entry.BorderColor = Color.FromHex("#5771F9");
						entry.BackgroundColor = Color.FromHex("#E9ECF1");
						entry.TextColor = Color.FromHex("#5771F9");
					}
					else
					{
						if (!VerifyCodePage.EntryHasText)
							Control.Background = VerifyCodeCustomEntry2Renderer.value;
						else
							if (VerifyCodePage.BFirstNumber)
							{
								entry.BorderColor = Color.FromHex("#5771F9");
								entry.BackgroundColor = Color.FromHex("#5771F9");
								entry.TextColor = Color.White;
							}
							else
							{
								entry.BorderColor = Xamarin.Forms.Color.FromHex("#E9ECF1");
								entry.BackgroundColor = Xamarin.Forms.Color.FromHex("#E9ECF1");
								entry.TextColor = Xamarin.Forms.Color.FromHex("#151940");
							}
					}
				}
			};

		}
	}
}