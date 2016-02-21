using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Escape.Droid
{
    [Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/launch")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate 
            {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}


