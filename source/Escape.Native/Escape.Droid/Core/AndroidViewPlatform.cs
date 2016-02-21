using System;
using Android.Content;
using Android.Content.PM;

namespace Escape.Droid
{
    public class AndroidViewPlatform : BaseClass, IViewPlatform
    {
        public AndroidViewPlatform(Context context)
            : base("AndroidViewPlatform")
        {
            this.Context = context;

            PackageInfo info = context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.MetaData);
            if (info != null)
            {
                this.VersionNumber = info.VersionName;
            }
            else
            {
                this.VersionNumber = "0.0";
            }
        }

        public virtual Context Context { get; set; }
        public virtual string VersionNumber { get; set; }
        public virtual string ShortName
        {
            get { return "android"; }
        }
    }
}
