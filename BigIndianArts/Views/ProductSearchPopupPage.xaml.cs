using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class ProductSearchPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ProductSearchPopupPage(string _search)
        {
            InitializeComponent();
            ent_SearchBox.Text = _search;
            ent_SearchBox.Focus();
        }
        protected override void OnAppearing()
        {
            ent_SearchBox.Focus();
            if (string.IsNullOrWhiteSpace(ent_SearchBox.Text))
            {
                ent_SearchBox.CursorPosition = 0;
            }
            else
            {
                ent_SearchBox.CursorPosition = ent_SearchBox.Text.Length;
            }
            string[] vs = { "a", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b" };
            cv_services.ItemsSource = vs;
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

       async private void Onsearching(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                await PopupNavigation.Instance.PopAsync();
                return;
            }


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

        private void ServiceListItem_Tapped(object sender, EventArgs e)
        {

        }

        private void WishList_Clicked(object sender, EventArgs e)
        {

        }
    }
}