﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CommunityToolkit.Mvvm.DependencyInjection;
using Screenbox.Core.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Screenbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumsPage : Page
    {
        internal AlbumsPageViewModel ViewModel => (AlbumsPageViewModel)DataContext;

        internal CommonViewModel Common { get; }

        public AlbumsPage()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<AlbumsPageViewModel>();
            Common = Ioc.Default.GetRequiredService<CommonViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.FetchAlbums();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModel.OnNavigatedFrom();
        }

        private void AlbumGridView_OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0) return;
            if (args.Item is AlbumViewModel album)
            {
                album.RelatedSongs[0].LoadThumbnailAsync();
            }
        }
    }
}
