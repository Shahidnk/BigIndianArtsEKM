using BigIndianArts.Views.FlyOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishListPage : ContentPage
    {
        public WishListPage()
        {
            InitializeComponent();
            string[] vs = { "a", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b" };
            cv_services.ItemsSource = vs;
        }

        private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlyoutShellPage());
        }

        private void Onsearching(object sender, TextChangedEventArgs e)
        {

        }

        private void OnFocusSearchButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnClearSearchClicked(object sender, EventArgs e)
        {

        }

        private void OnclickSerchbutton(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void WishList_Clicked(object sender, EventArgs e)
        {

        }

        private void ServiceListItem_Tapped(object sender, EventArgs e)
        {

        }
    }
}