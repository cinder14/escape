using System;

namespace Escape
{
    /// <summary>
    /// DI/IoC without a framework
    /// </summary>
    public static partial class Container
    {
        static Container()
        {
            Container.Track = new CoreTrack();
        }

        public static ITrack Track;
        public static IViewPlatform ViewPlatform;
        public static ICacheFileStore CacheStore;
        public static ICacheHost CacheHost;
        public static IEscapeApp EscapeApp;

        /// <summary>
        /// Not the standard IoC, but it'll work just fine
        /// </summary>
        public static void RegisterDependencies(ICacheFileStore cacheStore, ICacheHost cacheHost, IEscapeApp escapeApp, IViewPlatform platform)
        {
            Container.ViewPlatform = platform;
            Container.EscapeApp = escapeApp;
            Container.CacheStore = cacheStore;
            Container.CacheHost = cacheHost;
            Container.ViewPlatform = platform;
        }

    }
}

