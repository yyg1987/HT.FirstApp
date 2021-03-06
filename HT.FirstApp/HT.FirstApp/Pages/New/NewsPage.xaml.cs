﻿using System;
using System.Windows.Input;
using Xamarin.Forms;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using HT.FirstApp.ViewModel;
using HT.FirstApp.Pages.Account;

namespace HT.FirstApp.Pages.New
{
    public partial class NewsPage : ContentPage
    {
        NewsViewModel ViewModel => vm ?? (vm = BindingContext as NewsViewModel);
        NewsViewModel vm;
        public NewsPage(int position = 0) : base()
        {
            InitializeComponent();
            BindingContext = new NewsViewModel(position);
            this.NewsListView.ItemSelected += async delegate
            {
                var news = NewsListView.SelectedItem as News;
                if (news == null)
                    return;

                var newsDetails = new NewsDetailsPage(news);

                await NavigationService.PushAsync(Navigation, newsDetails);
                this.NewsListView.SelectedItem = null;
            };
            ViewModel.GetClientNewsAsync();
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