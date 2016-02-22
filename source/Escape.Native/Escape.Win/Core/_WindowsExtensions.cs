using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Escape.Win
{
    public static class _WindowsExtensions
    {

        #region Async Extensions
        public static void Await(this IAsyncAction operation)
        {
            try
            {
                var task = operation.AsTask();
                task.Wait();
            }
            catch (AggregateException exception)
            {
                // TODO - this possibly oversimplifies the problem report
                throw exception.InnerException;
            }
        }

        public static TResult Await<TResult>(this IAsyncOperation<TResult> operation)
        {
            try
            {
                var task = operation.AsTask();
                task.Wait();
                return task.Result;
            }
            catch (AggregateException exception)
            {
                // TODO - this possibly oversimplifies the problem report
                throw exception.InnerException;
            }
        }
        #endregion

        public static Frame GetFrame(this Window window)
        {
            return Window.Current.Content as Frame;
        }

        public static async Task NavigateAsync<T>(this Frame frame, bool clearHistory = false, Action<NavigationEventArgs> afterNavigated = null)
        {
            await frame.Dispatcher.RunAsync(global::Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                NavigatedEventHandler navigatedHandler = null;
                navigatedHandler = new NavigatedEventHandler(delegate (object sender, NavigationEventArgs args)
                {
                    frame.Navigated -= navigatedHandler;
                    if (clearHistory)
                    {
                        frame.BackStack.Clear();
                    }
                    if (afterNavigated != null)
                    {
                        afterNavigated(args);
                    }
                });
                frame.Navigated += navigatedHandler;
                frame.Navigate(typeof(T));
            });
        }
    }
}
