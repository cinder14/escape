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
	[Register ("MainController")]
	partial class MainController
	{
		[Outlet]
		UIKit.UIButton btnHello { get; set; }

		[Action ("btnHello_Click:")]
		partial void btnHello_Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnHello != null) {
				btnHello.Dispose ();
				btnHello = null;
			}
		}
	}
}
