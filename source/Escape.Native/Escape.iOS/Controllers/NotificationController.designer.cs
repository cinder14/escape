// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Escape.iOS
{
	[Register ("NotificationController")]
	partial class NotificationController
	{
		[Outlet]
		UIKit.UIButton btnFinish { get; set; }

		[Outlet]
		UIKit.UILabel lblBody { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Action ("btnFinish_Click:")]
		partial void btnFinish_Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnFinish != null) {
				btnFinish.Dispose ();
				btnFinish = null;
			}

			if (lblBody != null) {
				lblBody.Dispose ();
				lblBody = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}
