using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Escape.Win
{
    public class WindowsViewPlatform : BaseClass, IViewPlatform
    {
        public WindowsViewPlatform()
            : base("WindowsViewPlatform")
        {

        }
        public string VersionNumber
        {
            get
            {
                return Package.Current.Id.Version.ToString();
            }
        }
        public string ShortName
        {
            get
            {
                return "windows";
            }
        }
    }
}
