using Android.Widget;
using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using HT.FirstApp.Droid.Helpers;
using HT.FirstApp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(VersionCodeer))]
namespace HT.FirstApp.Droid.Helpers
{
    public class VersionCodeer : IVersionCode
    {
        public int GetVersionCode()
        {
            try
            {
                var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;
                var packageInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                return packageInfo.VersionCode;
            }
            catch (System.Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return 0;
        }
    }
}