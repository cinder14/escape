using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Escape
{
    public static class CoreUtility
    {
        public static bool ALLOW_DISPOSE_TRACE = true; // release mode never uses this, it is always disabled in release
        public static bool ALLOW_TRACING = false; // release mode never uses this, it is always disabled in release

        [Obsolete("Incorrect api call, use the Async Version of this method", true)]
        public static void ExecuteMethod(string name, Func<Task> method, Action<Exception> onError = null, bool supressMethodLogging = false)
        {
        }
        public static void ExecuteMethod(string name, Action method, Action<Exception> onError = null, bool supressMethodLogging = false)
        {
            #if DEBUG_METHODS
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("<{0}>", name));
            }
            #endif
            try
            {
                method();
            }
            catch (Exception ex)
            {
                Container.Track.LogError(ex, name);
                if (onError != null)
                {
                    onError(ex);
                }
                else
                {
                    HandleException(ex);
                }
            }
            #if DEBUG_METHODS
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("</{0}>", name));
            }
            #endif
        }
        public static async Task ExecuteMethodAsync(string name, Func<Task> method, Action<Exception> onError = null, bool supressMethodLogging = false)
        {
            #if DEBUG_METHODS
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("<{0}>", name));
            }
            #endif
            try
            {
                await method();
            }
            catch (Exception ex)
            {
                Container.Track.LogError(ex, name);
                if (onError != null)
                {
                    onError(ex);
                }
                else
                {
                    HandleException(ex);
                }
            }
            #if DEBUG_METHODS
            finally
            {
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("</{0}>", name));
            }
            }
            #endif
        }

        public static T ExecuteFunction<T>(string name, Func<T> method, Action<Exception> onError = null, bool supressMethodLogging = false)
        {
            #if DEBUG_METHODS
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("<{0}>", name));
            }
            #endif
            try
            {
                return method();
            }
            catch (Exception ex)
            {
                Container.Track.LogError(ex, name);
                if (onError != null)
                {
                    onError(ex);
                }
                else
                {
                    HandleException(ex);
                }
                return default(T);
            }
            #if DEBUG_METHODS
            finally
            {
            if(!supressMethodLogging)
            {
            LogMethodTrace(string.Format("</{0}>", name));
            }
            }
            #endif
        }
        public static async Task<T> ExecuteFunctionAsync<T>(string name, Func<Task<T>> method, Action<Exception> onError = null)
        {
            #if DEBUG_METHODS
            LogMethodTrace(string.Format("<{0}>", name));
            #endif
            try
            {
                return await method();
            }
            catch (Exception ex)
            {
                Container.Track.LogError(ex, name);
                if (onError != null)
                {
                    onError(ex);
                }
                else
                {
                    HandleException(ex);
                }
                return default(T);
            }
            #if DEBUG_METHODS
            finally
            {
            LogMethodTrace(string.Format("</{0}>", name));
            }
            #endif
        }

        public static void HandleException(Exception ex)
        {
            AggregateException aggregate = ex as AggregateException;
            if (aggregate != null)
            {
                foreach (var item in aggregate.InnerExceptions)
                {
                    HandleException(item);
                }
                return;
            }

            //TODO:COULD: Process Special Exception Types
            //IE: Catch all HTTP:429 errors, etc
        }

        private static void LogMethodTrace(string message)
        {
            #if DEBUG
            if (ALLOW_TRACING)
            {
                Container.Track.LogTrace(message);
            }
            #endif
        }
    }
}

