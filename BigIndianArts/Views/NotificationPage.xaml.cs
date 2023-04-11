using Acr.UserDialogs;
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
    public partial class NotificationPage : ContentPage
    {
        private string artistname;
        List<ArtistsModel> _artistsModels = new List<ArtistsModel>();
        public NotificationPage()
        {
            InitializeComponent();

        }
        async protected override void OnAppearing()
        {
            _artistsModels= await GetAllArtists();
            if (App._Role == "Admin")
            {
                await GetAllProducts();
            }
            else
            {
                await GetAllArtistRequest();
            }
        }
       async protected override void OnDisappearing()
        {
            await UpdateOrder();
        }
        public async Task GetAllArtistRequest()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
            NotificationModel notification = new NotificationModel();

            notification.type = "Artist";
            notification.id = Convert.ToInt32(App._LoggedPersonNumber);
            _apiresponse = await App._dataManager.GetNotification(notification);

            _apiresponse = await App._dataManager.GetNotification(notification);
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
                            cv_services.ItemsSource = _orders;
                            // DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _orders = _apiresponse.Data.ConvertAs<List<GetOrderModel>>();

                            if (_orders != null)
                            {
                                //foreach (var _item in _orders)
                                //{

                                //    if (!string.IsNullOrEmpty(_item.expected_date))
                                //    {
                                //        var _crDate = Convert.ToDateTime(_item.expected_date);
                                //        _item.expected_date = _crDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
                                //    }
                                //    if (!string.IsNullOrEmpty(_item.createdOn))
                                //    {
                                //        var _crDate = Convert.ToDateTime(_item.createdOn);
                                //        _item.createdOn = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                //    }
                                //    if (_item.status == "Pending")
                                //    {
                                //        _item.status = "Created";
                                //    }

                                //    if (_item.artist_read == 1)
                                //    {
                                //        _item.BgColour = Color.LightGray;
                                //    }
                                //    else
                                //    {
                                //        _item.BgColour = Color.White;
                                //    }
                                //}

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

                                    if (_item.status == "Pending")
                                    {
                                        _item.status = "Assigned to " + _artistsModels.Where(x => x.id == _item.user_id).FirstOrDefault().name;
                                        _item.icon = "newicon.png";
                                    }
                                    else if (_item.status == "Waiting For Approval")
                                    {
                                        _item.status = "Completed by " + _artistsModels.Where(x => x.id == _item.user_id).FirstOrDefault().name;
                                        _item.icon = "tickmark.png";
                                    }
                                    else
                                    {
                                        _item.icon = "order.png";
                                    }


                                    if (_item.admin_read == 1)
                                    {
                                        _item.BgColour = Color.LightGray;
                                    }
                                    else
                                    {
                                        _item.BgColour = Color.White;
                                    }
                                }

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
        public async Task UpdateOrder()
        {
            APIResponse _apiResponce = new APIResponse();
            NotificationModel _orderData = new NotificationModel();


            _orderData.id = 1;
            _orderData.type = App._Role;



            _apiResponce = await App._dataManager.UpdateOrderTask(_orderData);

            if (_apiResponce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Data Null");

                return;
            }

            if (_apiResponce.IsSuccess == true)
            {
                if (_apiResponce.Data == null)
                {
                    UserDialogs.Instance.HideLoading();

                    return;
                }

                var dat = _apiResponce.Data;

                // DependencyService.Get<IAlertPlayer>().AlertMessege("Order Updated Successfully.");
            }
        }

        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
            NotificationModel notification = new NotificationModel();

            notification.type = "Admin";
            notification.id = Convert.ToInt32(App._LoggedPersonNumber);
            _apiresponse = await App._dataManager.GetNotification(notification);
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
                            DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
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

                                    if (_item.status == "Pending")
                                    {
                                        _item.status = "Assigned to "+ _artistsModels.Where(x=>x.id==_item.user_id).FirstOrDefault().name;
                                        _item.icon = "newicon.png";
                                    }
                                    else if (_item.status == "Waiting For Approval")
                                    {
                                        _item.status = "Completed by " + _artistsModels.Where(x => x.id == _item.user_id).FirstOrDefault().name;
                                        _item.icon = "tickmark.png";
                                    }
                                    else
                                    {
                                        _item.icon = "order.png";
                                    }


                                    if (_item.admin_read == 1)
                                    {
                                        _item.BgColour = Color.LightGray;
                                    }
                                    else
                                    {
                                        _item.BgColour = Color.White;
                                    }
                                }


                                cv_services.ItemsSource = _orders;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }

        async public Task<List<ArtistsModel>> GetAllArtists()
        {
            APIResponse _apiresponse = new APIResponse();
            List<ArtistsModel> _artists = new List<ArtistsModel>();
            _apiresponse = await App._dataManager.GetAllArtists();
            try
            {
                if (_apiresponse == null)
                {
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    return null;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return null;
                        }
                        else
                        {
                            _artists = _apiresponse.Data.ConvertAs<List<ArtistsModel>>();

                            if (_artists != null)
                            {

                                foreach (var _item in _artists)
                                {

                                    if (!string.IsNullOrEmpty(_item.creted_on))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.creted_on).ToLocalTime();
                                        _item.dateCreated = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                        
                                    }
                                }

                              
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                        return null;
                    }
                    return _artists;
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return null;
            }
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ServiceListItem_Tapped(object sender, EventArgs e)
        {

        }

        private void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {

        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as GetOrderModel;
            _selectedBtn.BgColour = Color.LightGray;
           

            if (App._Role == "Admin")
            {
                await Navigation.PushAsync(new OrderDetailsPage(_selectedBtn.order_id, App._Role));
            }
            else
            {
                await Navigation.PushAsync(new ArtistOrderDetailsPage(_selectedBtn.order_id));
            }

        }
    }
}