using FormsToolkit;
using Newtonsoft.Json;
using System;

using Xamarin.Forms;
using HT.FirstApp.Helpers;
using HT.FirstApp.Interfaces;
using HT.FirstApp.Model;
using HT.FirstApp.ViewModel;
using HT.FirstApp.Pages.Account;

namespace HT.FirstApp.Pages.KbArticle
{
    public partial class KbArticlesDetailsPage : ContentPage
    {
        KbArticlesDetailsViewModel ViewModel => vm ?? (vm = BindingContext as KbArticlesDetailsViewModel);
        KbArticlesDetailsViewModel vm;
        KbArticles kbArticles;

        public KbArticlesDetailsPage(KbArticles kbArticles)
        {
            this.kbArticles = kbArticles;
            InitializeComponent();
            BindingContext = new KbArticlesDetailsViewModel(kbArticles);

            var cancel = new ToolbarItem
            {
                Text = "分享",
                Command = new Command(() =>
                {
                    DependencyService.Get<IShares>().Shares("http://kb.cnblogs.com/page/" + kbArticles.Id + "/", kbArticles.Title);
                })
            };
            ToolbarItems.Add(cancel);

            if (Device.Android == Device.RuntimePlatform)
                cancel.Icon = "toolbar_share.png";

            formsWebView.OnContentLoaded += delegate (object sender, EventArgs e)
            {
                RefreshKbArticles();
            };
        }
        async void RefreshKbArticles()
        {
            var question = JsonConvert.SerializeObject(await ViewModel.RefreshKbArticlesAsync());
            await formsWebView.InjectJavascriptAsync("updateModel(" + question + ");");
        }
        void OnReloadKbArticles(object sender, EventArgs args)
        {
            RefreshKbArticles();
        }
        async void OnBookmarks(object sender, EventArgs args)
        {
            if (UserTokenSettings.Current.HasExpiresIn())
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
            else
            {
                var url = "http://kb.cnblogs.com/page/" + kbArticles.Id + "/";
                await NavigationService.PushAsync(Navigation, new BookmarksEditPage(new Bookmarks() { Title = kbArticles.Title, LinkUrl = url, FromCNBlogs = true }));
            }
        }
    }
}