using BigIndianArts.CustomControls;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using BigIndianArts.Views;
using BigIndianArts.Views.FlyOut;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigIndianArts
{
    public partial class MainPage : ContentPage
    {
        private bool _textChanged;

        public MainPage()
        {
            InitializeComponent();
           // string[] vs = { "a", "b", "a", "b", "a", "b" };
            string[] vs1 = { "a", "b", "a", "b" };
           // cv_services.ItemsSource = vs;
            cv_trending.ItemsSource = vs1;
          
           
        }
        async protected override void OnAppearing()
        {

            List<RotatorModel> list = new List<RotatorModel>();

            list.Add(new RotatorModel() { Image = "demo1.jpg", Id = 1, RequestId = 2 });
            list.Add(new RotatorModel() { Image = "demo2.jpg", Id = 3, RequestId = 2 });
            list.Add(new RotatorModel() { Image = "demo3.jpg", Id = 2, RequestId = 2 });





            sfRotator_Home.ItemsSource = list;



            await GetAllProducts();

        }
        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<ProdectModel> _prodects = new List<ProdectModel>();
            _apiresponse = await App._dataManager.GetAllProducts();
            try
            {
                if (_apiresponse == null)
                {
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    return;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _prodects = _apiresponse.Data.ConvertAs<List<ProdectModel>>();

                            if (_prodects != null)
                            {
                                cv_services.ItemsSource = _prodects;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        async private void ImageNotifyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationPage());
        }

       async private void TapGestureProfileRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage());
        }

        private void OnclickSerchbutton(object sender, EventArgs e)
        {

        }

        private void OnFocusSearchButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnClearSearchClicked(object sender, EventArgs e)
        {

        }

        private void homemenutapped(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;

        }

       async private void Onsearching(object sender, TextChangedEventArgs e)
        {
            if (_textChanged)
            {
                return;
            }

            _textChanged = true;
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                _textChanged = false;
                return;

            }

            var ProductSearch = new ProductSearchPopupPage(e.NewTextValue);
           
           await PopupNavigation.Instance.PushAsync(ProductSearch);
            _textChanged = false;
        }

        async private void ServiceListItem_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductDetailPage());
        }

       async private void kart_clicked(object sender, EventArgs e)
        {
           
        }
        async private void user_clicked(object sender, EventArgs e)
        {
           
        }

        private void BannerImg_clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void WishList_Clicked(object sender, EventArgs e)
        {

        }
    }
}
