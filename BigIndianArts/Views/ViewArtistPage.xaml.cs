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
    public partial class ViewArtistPage : ContentPage
    {
        public ViewArtistPage()
        {
            InitializeComponent();
           
        }
       async protected override void OnAppearing()
        {
            await GetAllArtists();
        }
        public async Task GetAllArtists()
        {
            APIResponse _apiresponse = new APIResponse();
            List<ArtistsModel> _artists = new List<ArtistsModel>();
            _apiresponse = await App._dataManager.GetAllArtists();
            try
            {
                if (_apiresponse == null)
                {
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    cv_orders.ItemsSource = null;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                            cv_orders.ItemsSource = null;
                            
                        }
                        else
                        {
                            _artists = _apiresponse.Data.ConvertAs<List<ArtistsModel>>();

                            if (_artists != null)
                            {

                                foreach(var _item in _artists)
                                {

                                    if (!string.IsNullOrEmpty(_item.creted_on))
                                    {
                                        var _crDate=Convert.ToDateTime(_item.creted_on).ToLocalTime();
                                        _item.dateCreated = _crDate.ToString("dd MMMM yyyy hh:mm tt");

                                        if (string.IsNullOrEmpty(_item.amount))
                                        {
                                            _item.amount = "0";
                                        }
                                    }
                                }

                                

                                cv_orders.ItemsSource = _artists;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                        cv_orders.ItemsSource = null;
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                cv_orders.ItemsSource = null;
             
            }
        }
        async private void Cv_selectionChanged(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as ArtistsModel;
            await Navigation.PushAsync(new ArtistMenuPage(_selectedBtn.id, _selectedBtn));
        }

        async private void CreateOrder_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateArtistsPage());
        }

       async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        private void call_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var _data = btn.BindingContext as ArtistsModel;

            try
            {
                PhoneDialer.Open(_data.number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}