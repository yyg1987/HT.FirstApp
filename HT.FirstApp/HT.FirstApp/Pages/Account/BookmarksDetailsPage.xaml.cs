using Xamarin.Forms;
using HT.FirstApp.Interfaces;
using HT.FirstApp.Model;

namespace HT.FirstApp.Pages.Account
{
    public partial class BookmarksDetailsPage : ContentPage
    {
        public BookmarksDetailsPage(Bookmarks bookmarks) : base()
        {
            InitializeComponent();
            Title = bookmarks.Title;

            var cancel = new ToolbarItem
            {
                Text = "分享",
                Command = new Command(() =>
                {
                    DependencyService.Get<IShares>().Shares(bookmarks.LinkUrl, bookmarks.Title);
                })
            };
            ToolbarItems.Add(cancel);

            if (Device.Android == Device.RuntimePlatform)
                cancel.Icon = "toolbar_share.png";

            authorizeView.Source = bookmarks.LinkUrl;
        }
    }
}