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
	[Register ("CreateController")]
	partial class CreateController
	{
		[Outlet]
		UIKit.UITextField txtBody { get; set; }

		[Outlet]
		UIKit.UITextField txtTitle { get; set; }

		[Outlet]
		UIKit.UITextField txtWhen { get; set; }

		[Action ("btnCreate_Click:")]
		partial void btnCreate_Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (txtWhen != null) {
				txtWhen.Dispose ();
				txtWhen = null;
			}

			if (txtTitle != null) {
				txtTitle.Dispose ();
				txtTitle = null;
			}

			if (txtBody != null) {
				txtBody.Dispose ();
				txtBody = null;
			}
		}
	}
}
