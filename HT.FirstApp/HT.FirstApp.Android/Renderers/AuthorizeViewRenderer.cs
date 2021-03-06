﻿using Android.Content;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Xam.Plugin.WebView.Droid;
using Xamarin.Forms;
using HT.FirstApp.Droid.Renderers;
using HT.FirstApp.Controls;

[assembly: ExportRenderer(typeof(AuthorizeView), typeof(AuthorizeViewRenderer))]
namespace HT.FirstApp.Droid.Renderers
{
    public class AuthorizeViewRenderer : FormsWebViewRenderer
    {
        public AuthorizeViewRenderer() : base()
        {
            var cookieSyncManager = CookieManager.Instance;

            cookieSyncManager.SetAcceptCookie(true);
            cookieSyncManager.RemoveSessionCookie();
            cookieSyncManager.RemoveAllCookie();
        }
    }
}