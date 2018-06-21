using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using HT.FirstApp.ViewModel;

namespace HT.FirstApp.Pages.KbArticle
{
	public partial class KbArticlesPage : ContentPage
    {
        KbArticlesViewModel ViewModel => vm ?? (vm = BindingContext as KbArticlesViewModel);
        KbArticlesViewModel vm;
        public KbArticlesPage()
        {
            InitializeComponent();
            BindingContext = new KbArticlesViewModel();

            Title = "知识库";

            this.KbArticlesListView.ItemSelected += async delegate
            {
                var kbarticles = KbArticlesListView.SelectedItem as KbArticles;
                if (kbarticles == null)
                    return;

                var kbarticlesDetails = new KbArticlesDetailsPage(kbarticles);

                await NavigationService.PushAsync(Navigation, kbarticlesDetails);

                this.KbArticlesListView.SelectedItem = null;
            };
            ViewModel.GetClientKbArticlesAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdatePage();
        }
        private void UpdatePage()
        {
            bool forceRefresh = (DateTime.Now > (ViewModel?.NextRefreshTime ?? DateTime.Now));

            if (forceRefresh)
            {
                //刷新
                ViewModel.RefreshCommand.Execute(null);
            }
        }
    }
}