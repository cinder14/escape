using System;

namespace Escape
{
    public class EscapeApp : BaseClass, IEscapeApp
	{
        #region Constructor

        public EscapeApp(ICacheHost cacheHost, IViewPlatform viewPlatform)
            : base("EscapeApp")
        {
            this.CacheHost = cacheHost;
            this.ViewPlatform = viewPlatform;
        }

        #endregion

        #region Properties

        public ICacheHost CacheHost { get; protected set; }
        public IViewPlatform ViewPlatform { get; protected set; }

        #endregion

        public void Initialize()
        {
            
        }
	}
}

