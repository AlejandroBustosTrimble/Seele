using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Helpers
{
    /// <summary>
    /// Helper de acciones
    /// </summary>
    public static class ActionHelper
    {
        /// <summary>
        /// Ejecuta una accion en el thread principal
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task BeginInvokeOnMainThreadAsync(Action action)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        public static Task BeginInvokeOnMainThreadAsync(Task action)
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(
                async () =>
                {
                    try
                    {
                        await action;
                        tcs.SetResult(null);
                    }
                    catch (Exception e)
                    {
                        tcs.SetException(e);
                    }
                });

            return tcs.Task;
        }
    }
}
