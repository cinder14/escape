using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escape
{
    public abstract partial class BaseClass : IDisposable
    {
        #region Constructor

        public BaseClass(string trackPrefix)
        {
            this.TrackPrefix = trackPrefix;
        }
        ~BaseClass()
        {
            this.Dispose(false);
        }

        #endregion

        #region Protected Properties

        protected string TrackPrefix { get; set; }

        #endregion

        #region Dispose Methods

        protected virtual void Dispose(bool disposing)
        {
            
        }
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion

        #region Log Methods

        protected virtual void LogWarning(string message, string tag = "")
        {
            Container.Track.LogWarning(this.TrackPrefix + ":" + tag + ": " + message);
        }
        protected virtual void LogTrace(string message, string tag = "")
        {
            Container.Track.LogTrace(this.TrackPrefix + ":" + tag + ": " + message);
        }
        protected virtual void LogError(Exception ex, string tag = "")
        {
            Container.Track.LogError(ex, this.TrackPrefix + ":" + tag);
        }

        #endregion
    }
}