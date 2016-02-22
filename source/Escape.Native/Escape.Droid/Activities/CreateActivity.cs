
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Newtonsoft.Json;
using Java.Interop;

namespace Escape.Droid
{
    [Activity(Label = "CreateActivity")]			
    public class CreateActivity : Activity, ICreateView
    {
        #region Properties

        public CreateViewModel ViewModel { get; set; }

        #endregion

        #region Overrides

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.Create);

            this.ViewModel = new CreateViewModel(this);
        }

        #endregion

        #region View Methods

        public void EnsureNotificationPermission()
        {
            // don't need to do this, its at the manifest level
        }

        protected void Create()
        {
            try
            {
                TextView txtTitle = this.FindViewById<TextView>(Resource.Id.rescue_title);
                TextView txtBody = this.FindViewById<TextView>(Resource.Id.rescue_body);
                TextView txtWhen = this.FindViewById<TextView>(Resource.Id.rescue_when);

                DateTime parsed = default(DateTime);
                if(!DateTime.TryParse(txtWhen.Text, out parsed) || parsed == default(DateTime))
                {
                    throw new Exception("You must provide a valid date");
                }
                Rescue rescue = new Rescue()
                {
                    rescue_id = Guid.NewGuid(),
                    stamp_utc = parsed,
                    title = txtTitle.Text,
                    body = txtBody.Text
                };

                TimeSpan fromNow =  rescue.stamp_utc - DateTime.Now;

                Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
                alarmIntent.PutExtra("rescue", JsonConvert.SerializeObject(rescue));

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

                AlarmManager alarmManager = (AlarmManager)this.GetSystemService(Context.AlarmService);
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + (long)fromNow.TotalMilliseconds, pendingIntent);

                this.ViewModel.AddRescue(rescue);

                //TODO:Could:Notify success first.

                this.Finish();
            }
            catch (Exception ex)
            {
                new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetTitle("Error")
                    .SetMessage(ex.Message)
                    .SetPositiveButton("Okay", new EventHandler<DialogClickEventArgs>((s, e) => {}))
                    .Show();
            }

        }

        #endregion

        #region Event Handlers

        [Export("btnCreate_Click")]
        public void btnCreate_Click(View view)
        {
            this.Create();
        }

        #endregion
    }
}

