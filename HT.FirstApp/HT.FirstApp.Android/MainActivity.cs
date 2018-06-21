using System;
using Com.Umeng.Socialize;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xam.Plugin.WebView.Droid;
using FormsToolkit.Droid;
using FFImageLoading.Forms.Droid;
using HT.FirstApp.Droid.Helpers;
using Com.Tencent.Android.Tpush;
using Android.Content;

namespace HT.FirstApp.Droid
{
    [Activity(Exported = true,
        LaunchMode = LaunchMode.SingleTask,
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IXGIOperateCallback
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FormsWebViewRenderer.Initialize();
            Toolkit.Init();
            CachedImageRenderer.Init(true);

            Shares.Init(this);

            XGPushConfig.EnableDebug(this, !BuildConfig.Debug);
            XGPushManager.RegisterPush(this, this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        public void OnFail(Java.Lang.Object data, int flag, string message)
        {
        }

        public void OnSuccess(Java.Lang.Object data, int flag)
        {
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            UMShareAPI.Get(this).OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}

