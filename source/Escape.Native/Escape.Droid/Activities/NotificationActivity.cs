using System;
using Android.App;
using Android.OS;
using Newtonsoft.Json;
using Android.Widget;
using Java.Interop;
using Android.Views;

namespace Escape.Droid
{
    [Activity (Label = "@string/app_name", Icon = "@drawable/launch")]
    public class NotificationActivity : Activity, IViewModelView
    {
        #region Overrides

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            this.SetContentView(Resource.Layout.Notification);

            string rescueString = this.Intent.GetStringExtra("rescue");
            Rescue rescue = JsonConvert.DeserializeObject<Rescue>(rescueString);

            TextView txtTitle = this.FindViewById<TextView>(Resource.Id.general_title);
            TextView txtBody = this.FindViewById<TextView>(Resource.Id.general_body);

            txtTitle.Text = rescue.title;
            txtBody.Text = rescue.body;
        }

        #endregion

        #region Event Handlers

        [Export("btnDismiss_Click")]
        public void btnDismiss_Click(View view)
        {
            this.Finish();
        }

        #endregion
    }
}


