using System;
using Foundation;

namespace Escape.iOS
{
    public class IOSViewPlatform : BaseClass, IViewPlatform
    {
        public IOSViewPlatform()
            : base("IOSViewPlatform")
        {
            NSString version = new NSString("CFBundleShortVersionString");
            if(NSBundle.MainBundle.InfoDictionary.ContainsKey(version))
            {
                this.VersionNumber = NSBundle.MainBundle.InfoDictionary[version].ToString();
            }
            else
            {
                this.VersionNumber = "0.0";
            }
        }
        public virtual string VersionNumber { get; set; }

        public virtual string ShortName
        {
            get { return "ios"; }
        }
    }
}