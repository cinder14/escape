
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
using System.Threading.Tasks;
using UniversalImageLoader.Core;
using Android.Graphics;
using UniversalImageLoader.Core.Assist;
using UniversalImageLoader.Core.Display;

namespace Escape.Droid
{
    #if DEBUG
    [Application(Debuggable = true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class EscapeApplication : Application
    {
        public EscapeApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            
            // <Fake ioc>
            ICacheFileStore fileStore = new AndroidFileStore(this);
            ICacheHost cacheHost = new CacheHost(CoreAssumptions.INTERNAL_APP_NAME);
            IViewPlatform viewPlatform = new AndroidViewPlatform(this);
            IEscapeApp escapeApp = new EscapeApp(cacheHost, viewPlatform);
            Container.RegisterDependencies(fileStore, cacheHost, escapeApp, viewPlatform);
            // </Fake ioc>


            // Simple bootstrap
            Container.EscapeApp.Initialize();

        }

        public override void OnCreate()
        {
            CoreUtility.ExecuteMethod("EscapeApplication.OnCreate", delegate()
            {
                base.OnCreate();

                this.InitializeEnvironment();
            });
        }

        protected void InitializeEnvironment()
        {
            CoreUtility.ExecuteMethod("InitializeEnvironment", delegate()
            {
                // Initialize Image Loader
                DisplayImageOptions defaultOptions = new DisplayImageOptions.Builder()
                    .BitmapConfig(Bitmap.Config.Rgb565)
                    .ImageScaleType(ImageScaleType.Exactly)
                    .CacheOnDisk(true)
                    .CacheInMemory(true)
                    .Displayer(new FadeInBitmapDisplayer(200))
                    .Build();
                ImageLoaderConfiguration config = new ImageLoaderConfiguration.Builder(BaseContext)
                    .DefaultDisplayImageOptions(defaultOptions)
                    .DiskCacheExtraOptions(480, 320, null)
                    .Build();
                ImageLoader.Instance.Init(config);
                Container.ImageLoader = ImageLoader.Instance;
            });
        }
       
    }
}