// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Escape.iOS
{
	public partial class NotificationController : UIViewController
	{
		public NotificationController (IntPtr handle) : base (handle)
		{
		}


        public const string IDENTIFIER = "NotificationController";

        public Rescue Rescue { get; set; }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblBody.Text = this.Rescue.title;
            lblBody.Text = this.Rescue.body;
        }

        partial void btnFinish_Click(NSObject sender)
        {
            EscapeAppDelegate.Current.LaunchMain(UIViewAnimationOptions.TransitionFlipFromLeft);
        }
	}
}
