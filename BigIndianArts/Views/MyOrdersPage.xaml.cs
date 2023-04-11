using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
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
    public partial class MyOrdersPage : ContentPage
    {
        public MyOrdersPage()
        {
            InitializeComponent();
            
        }
       async protected override void OnAppearing()
        {
            await GetAllProducts();
        }
        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
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
                            _orders = _apiresponse.Data.ConvertAs<List<GetOrderModel>>();

                            if (_orders != null)
                            {
                                cv_services.ItemsSource = _orders;
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
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

      async  private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderHistoryPage());
        }

        private void WishList_Clicked(object sender, EventArgs e)
        {

        }

       async private void ServiceListItem_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderHistoryPage());
        }
    }
}