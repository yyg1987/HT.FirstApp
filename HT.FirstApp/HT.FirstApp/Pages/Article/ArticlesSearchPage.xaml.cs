﻿using System;
using Xamarin.Forms;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using HT.FirstApp.ViewModel;
namespace HT.FirstApp.Pages.Article
{
    public partial class ArticlesSearchPage : ContentPage
    {
        SearchViewModel ViewModel => vm ?? (vm = BindingContext as SearchViewModel);
        SearchViewModel vm;
        public ArticlesSearchPage() : base()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel(0);
            this.SearchsListView.ItemSelected += async delegate
            {
                var search = SearchsListView.SelectedItem as Search;
                if (search == null)
                    return;
                var articles = new Articles()
                {
                    Author = search.UserName,
                    Avatar = search.UserName,
                    BlogApp = search.UserAlias,
                    Body = "",
                    CommentCount = search.CommentTimes,
                    Description = search.Content,
                    DiggCount = search.VoteTimes,
                    Id = int.Parse(search.Id),
                    PostDate = search.PublishTime,
                    Title = search.Title.Replace("<strong>", "").Replace("</strong>", ""),
                    Url = search.Uri,
                    ViewCount = search.ViewTimes
                };
                var articlesDetails = new ArticlesDetailsPage(articles);

                await NavigationService.PushAsync(Navigation, articlesDetails);
                this.SearchsListView.SelectedItem = null;
            };
        }
    }
}