using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using HT.FirstApp.Droid.Helpers;
using HT.FirstApp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Toaster))]
namespace HT.FirstApp.Droid.Helpers
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;
            Device.BeginInvokeOnMainThread(() =>
            {
                Toast.MakeText(context, message, ToastLength.Long).Show();
            });
        }
    }
}