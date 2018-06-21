using Naxam.Controls.Forms;

using Xamarin.Forms;
using HT.FirstApp.Helpers;
using HT.FirstApp.Interfaces;
using HT.FirstApp.Pages.About;
using HT.FirstApp.Pages.KbArticle;

namespace HT.FirstApp.Pages.Article
{
    public class ArticlesTopTabbedPage : TopTabbedPage
    {
        public ArticlesTopTabbedPage()
        {
            BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            BarIndicatorColor = (Color)Application.Current.Resources["Divider"];
            BarBackgroundColor = (Color)Application.Current.Resources["Primary"];

            Title = "首页";
            Icon = "menu_home.png";
            this.Children.Add(new ArticlesPage() { Title = "博客" });
            this.Children.Add(new ArticlesPage(1) { Title = "精华" });
            this.Children.Add(new KbArticlesPage() { Title = "知识库" });

            var cancel = new ToolbarItem
            {
                Text = "搜索",
                Command = new Command(async () =>
                {
                    await NavigationService.PushAsync(Navigation, new ArticlesSearchPage());
                })
            };
            ToolbarItems.Add(cancel);

            if (Device.Android == Device.RuntimePlatform)
                cancel.Icon = "toolbar_search.png";
        }
    }
}