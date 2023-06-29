using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.Triggers
{
	public class ClickedViewPasswordImageSourceTrigger: TriggerAction<ImageButton>
	{

		private const string source= "File: RegisterIsPasswordTrue";

		#region Invoke

		protected override void Invoke(ImageButton button)
		{
			if (button.Source.ToString() == source)
			{
				button.Source = "RegisterIsPasswordFalse";
				button.BindingContext = " ";
			}
			else
			{
				button.Source = "RegisterIsPasswordTrue";
				button.BindingContext = "";
			}
		}

		#endregion Invoke
	}
}
