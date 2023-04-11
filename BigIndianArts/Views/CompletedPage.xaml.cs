using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedPage : ContentPage
    {
        private string artist_id;

        public CompletedPage(string id)
        {
            InitializeComponent();
            artist_id = id;
        }
        async protected override void OnAppearing()
        {
            await GetAllProducts();
        }
        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
            _apiresponse = await App._dataManager.GetAllOrders();
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
                                foreach (var _item in _orders)
                                {

                                    if (!string.IsNullOrEmpty(_item.expected_date))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.expected_date);
                                        _item.expected_date = _crDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
                                    }
                                    if (!string.IsNullOrEmpty(_item.createdOn))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.createdOn);
                                        _item.createdOn = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                    }
                                }

                                if (App._Role == "Artist")
                                {
                                    var _data = _orders.Where(x => x.assign_id == App._LoggedPersonNumber.ToString());
                                    cv_Gallery.ItemsSource = _data.Where(x => x.status == "Completed");
                                }
                                if (App._Role == "Admin")
                                {
                                    var _data = _orders.Where(x => x.assign_id == artist_id.ToString());
                                    cv_Gallery.ItemsSource = _data.Where(x => x.status == "Completed");
                                }
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

        async private void Cv_selectionChanged(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as GetOrderModel;
            if (App._Role == "Artist")
            {
                await Navigation.PushAsync(new ArtistOrderDetailsPage(_selectedBtn.id));
            }
            if (App._Role == "Admin")
            {
                await Navigation.PushAsync(new OrderDetailsPage(_selectedBtn.id,App._Role));
            }
           
           // await Browser.OpenAsync(_selectedBtn.final_image, BrowserLaunchMode.SystemPreferred);
        }
    }
}