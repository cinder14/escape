using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Java.Interop;

namespace Escape.Droid
{
    [Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/launch")]
    public class MainActivity : Activity, IViewModelView
	{
        #region Properties

        public DashboardViewModel ViewModel { get; set; }

        #endregion

        #region Overrides

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			this.SetContentView(Resource.Layout.Main);

            this.ViewModel = new DashboardViewModel(this);

            ListView lstView = this.FindViewById<ListView>(Resource.Id.general_list);
            lstView.Adapter = new DashboardAdapter(this, this.ViewModel.Rescues);

            this.ViewModel.Start();

		}

        protected override void OnResume()
        {
            base.OnResume();

            ListView lstView = this.FindViewById<ListView>(Resource.Id.general_list);
            lstView.Adapter = new DashboardAdapter(this, this.ViewModel.Rescues);
        }

        #endregion

        #region Event Handlers

        [Export("btnCreate_Click")]
        public void btnCreate_Click(View view)
        {
            this.StartActivity(typeof(CreateActivity));
        }

        #endregion

        public class DashboardAdapter : BaseAdapter<Rescue>
        {
            public DashboardAdapter(Activity activity, List<Rescue> data)
            {
                this.Context = activity;
                this.Data = data;
            }

            public Activity Context { get; set; }
            public List<Rescue> Data { get; set; }

            public override long GetItemId(int position)
            {
                return position;
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View result = convertView;
                if (result == null)
                {
                    result = this.Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
                }
                TextView txtView = result.FindViewById<TextView>(Android.Resource.Id.Text1);
                txtView.Text = this.Data[position].title;

                return result;
            }

            public override int Count
            {
                get
                {
                    return this.Data.Count;
                }
            }

            public override Rescue this[int index]
            {
                get
                {
                    return this.Data[index];
                }
            }
        }
	}
}


