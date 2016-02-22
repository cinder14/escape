using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Escape.Win.Pages
{
    public sealed partial class MainPage : Page, IViewModelView
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public DashboardViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel = new DashboardViewModel(this);

            lstData.ItemsSource = this.ViewModel.Rescues;

            this.ViewModel.Start();
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreatePage));
        }
    }
}
