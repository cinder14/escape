using Foundation;
using UIKit;

namespace Escape.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("EscapeAppDelegate")]
	public class EscapeAppDelegate : UIApplicationDelegate
	{
        public static EscapeAppDelegate Current
        {
            get
            {
                return UIApplication.SharedApplication.Delegate as EscapeAppDelegate;
            }
        }

        public virtual UIStoryboard MainStoryboard { get; set; }
        public override UIWindow Window { get; set; }

        #region Overrides

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
            
            // <Fake ioc>
            ICacheFileStore fileStore = new IOSFileStore();
            ICacheHost cacheHost = new CacheHost(CoreAssumptions.INTERNAL_APP_NAME);
            IViewPlatform viewPlatform = new IOSViewPlatform();
            IEscapeApp escapeApp = new EscapeApp(cacheHost, viewPlatform);
            Container.RegisterDependencies(fileStore, cacheHost, escapeApp, viewPlatform);
            // </Fake ioc>

            Container.EscapeApp.Initialize();



            this.InitializeEnvironment();

            this.MainStoryboard = UIStoryboard.FromName("MainStoryboard", null);
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

            this.LaunchMain();

            this.Window.MakeKeyAndVisible();

            return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

        #endregion

        #region App Methods

        public virtual void LaunchMain(UIViewAnimationOptions transition = UIViewAnimationOptions.TransitionFlipFromLeft)
        {
            CoreUtility.ExecuteMethod("LaunchMain", delegate()
            {
                UIViewController firstController = this.MainStoryboard.InstantiateViewController(MainController.IDENTIFIER);
                UINavigationController navController = new UINavigationController(firstController);
                navController.NavigationBarHidden = true;
                this.ChangeRootViewController(navController, transition);
            });
        }

        protected virtual void ChangeRootViewController(UIViewController newController, UIViewAnimationOptions transition)
        {
            CoreUtility.ExecuteMethod("ChangeRootViewController", delegate()
            {
                UIViewController previousRoot = this.Window.RootViewController;
                if (this.Window.RootViewController == null)
                {
                    this.Window.RootViewController = newController;
                }
                else
                {
                    UIView.Transition(this.Window, 0.5, transition, delegate()
                    {
                        this.Window.RootViewController = newController;
                    }, null);
                }
                if (previousRoot != null)
                {
                    previousRoot.Dispose();
                }
            });
        }

        protected virtual void InitializeEnvironment()
        {
            CoreUtility.ExecuteMethod("InitializeEnvironment", delegate()
            {
                // nothing yet.
            });
        }


        #endregion
	}
}


