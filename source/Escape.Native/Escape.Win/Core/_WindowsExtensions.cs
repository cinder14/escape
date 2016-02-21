using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        public static async Task GoBackAsync(this Frame frame)
        {
            if (frame != null)
            {
                await frame.Dispatcher.RunAsync(global::Windows.UI.Core.CoreDispatcherPriority.Normal, () => frame.GoBack());
            }
        }

    }
}
