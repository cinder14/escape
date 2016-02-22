using System;
using Android.Content;
using Android.Support.V4.App;
using Newtonsoft.Json;
using Android.App;

namespace Escape.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public AlarmReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            //TODO:SHOULD: Verify Intent Extra
            string rescueString = intent.GetStringExtra("rescue");
            Rescue rescue = JsonConvert.DeserializeObject<Rescue>(rescueString);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetStyle(new NotificationCompat.InboxStyle())
                .SetSmallIcon(Resource.Drawable.launch)
                .SetContentTitle(rescue.title)
                .SetContentText(rescue.body);

            Intent resultIntent = new Intent(context, typeof(NotificationActivity));
            resultIntent.PutExtra("rescue", rescueString);
            PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
    
            //TODO:MUST: Inject the back stack so that the back button works as we want
            builder.SetContentIntent(resultPendingIntent);

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            notificationManager.Notify(0, builder.Build());
        }
       
    }
}

