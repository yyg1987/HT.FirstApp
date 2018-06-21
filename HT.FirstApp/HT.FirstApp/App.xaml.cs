using FormsToolkit;
using HT.FirstApp.Controls;
using HT.FirstApp.Helpers;
using HT.FirstApp.Interfaces;
using HT.FirstApp.Model;
using HT.FirstApp.Pages.Account;
using HT.FirstApp.Pages.Article;
using HT.FirstApp.Pages.KbArticle;
using HT.FirstApp.Pages.New;
using HT.FirstApp.Pages.Question;
using HT.FirstApp.Pages.Status;
using HT.FirstApp.ViewModel;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HT.FirstApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SqliteUtil.Current.CreateAllTablesAsync();

            AppCenter.Start("android=72a75e57-6edb-4ba2-aee6-4cf0ddfba501;ios=3fc5d636-dc00-46cb-b7a6-b685749e6e77;", typeof(Analytics), typeof(Crashes));

            ViewModelBase.Init();

            XamBottomBarPage bottomBarPage = new XamBottomBarPage() { Title = "博客园" };

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                bottomBarPage.BarTextColor = (Color)Application.Current.Resources["Primary"];
                bottomBarPage.FixedMode = true;
                bottomBarPage.BarTheme = XamBottomBarPage.BarThemeTypes.Light;
            }

            bottomBarPage.Children.Add(new ArticlesTopTabbedPage());
            bottomBarPage.Children.Add(new NewsTopTabbedPage());
            bottomBarPage.Children.Add(new StatusesTopTabbedPage());
            bottomBarPage.Children.Add(new QuestionsTopTabbedPage());
            bottomBarPage.Children.Add(new AccountPage());

            MainPage = new XamNavigationPage(bottomBarPage);
            //MainPage = new NavigationPage(new Page1() { Title = "测试" });
        }

        protected override void OnStart()
        {
            OnResume();
        }
        bool registered;
        protected async override void OnResume()
        {
            await UserTokenSettings.Current.RefreshUserTokenAsync();

            if (registered)
                return;
            registered = true;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;

            MessagingService.Current.Subscribe(MessageKeys.NavigateLogin, async m =>
            {
                Page page = new NavigationPage(new AuthorizePage());

                var nav = Application.Current?.MainPage?.Navigation;
                if (nav == null)
                    return;

                await NavigationService.PushModalAsync(nav, page);
            });
            MessagingService.Current.Subscribe<string>(MessageKeys.NavigateToken, async (m, q) =>
            {
                var result = await TokenHttpClient.Current.PostTokenAsync(q);
                if (result.Success)
                {
                    var token = JsonConvert.DeserializeObject<Token>(result.Message.ToString());
                    token.RefreshTime = DateTime.Now;
                    UserTokenSettings.Current.UpdateUserToken(token);

                    var userResult = await UserHttpClient.Current.GetAsyn(Apis.Users);
                    if (userResult.Success)
                    {
                        var user = JsonConvert.DeserializeObject<User>(userResult.Message.ToString());

                        UserSettings.Current.UpdateUser(user);

                        var nav = Application.Current?.MainPage?.Navigation;
                        if (nav == null)
                            return;
                        await nav.PopModalAsync();
                    }
                }
            });
            MessagingService.Current.Subscribe(MessageKeys.NavigateAccount, async m =>
            {
                Page page = new NavigationPage(new AccountPage());

                var nav = Application.Current?.MainPage?.Navigation;
                if (nav == null)
                    return;

                await NavigationService.PushModalAsync(nav, page);
            });
            MessagingService.Current.Subscribe<string>(MessageKeys.NavigateNotification, async (m, message) =>
            {
                DependencyService.Get<IToast>().SendToast(message);
                var nav = Application.Current?.MainPage?.Navigation;
                if (nav == null)
                    return;
                Page page = null;
                var notification = JsonConvert.DeserializeObject<Notification>(message);
                if (notification != null)
                {
                    switch (notification.Type)
                    {
                        case "articles":
                            page = new NavigationPage(new ArticlesDetailsPage(new Articles() { Title = notification.Title, Id = notification.ID }));
                            break;
                        case "news":
                            page = new NavigationPage(new NewsDetailsPage(new News() { Title = notification.Title, Id = notification.ID }));
                            break;
                        case "kbarticles":
                            page = new NavigationPage(new KbArticlesDetailsPage(new KbArticles() { Title = notification.Title, Id = notification.ID }));
                            break;
                        case "questions":
                            page = new NavigationPage(new QuestionsDetailsPage(new Questions() { Title = notification.Title, Qid = notification.ID }));
                            break;
                        case "update":
                            var versionCode = DependencyService.Get<IVersionCode>().GetVersionCode();
                            if (notification.ID > versionCode)
                            {
                                if (await Application.Current?.MainPage.DisplayAlert("新版提示", notification.Title, "立即下载", "取消"))
                                {
                                    await ViewModelBase.ExecuteLaunchBrowserAsync(notification.Url);
                                }
                            }
                            return;
                        default:
                            return;
                    }
                }
                await NavigationService.PushAsync(nav, page);
            });
        }
        protected override void OnSleep()
        {
            if (!registered)
                return;

            registered = false;
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateLogin);
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateToken);
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateAccount);
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateNotification);

            CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }

        protected async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DependencyService.Get<IToast>().SendToast("网络不给你，请检查网络设置");
            }
            else
            {
                await UserTokenSettings.Current.RefreshUserTokenAsync();
            }
        }
    }
}
