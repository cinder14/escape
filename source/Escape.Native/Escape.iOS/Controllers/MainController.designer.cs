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
		UIKit.UITableView tblData { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblData != null) {
				tblData.Dispose ();
				tblData = null;
			}
		}
	}
}
