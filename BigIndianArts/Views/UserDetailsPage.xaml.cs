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
    public partial class UserDetailsPage : ContentPage
    {
        private string _uid;

        public UserDetailsPage(ArtistsModel artist)
        {
            InitializeComponent();
            _uid = artist.id;
            //lbl_address.Text = artist.address;
            //sfAvatar_profileImg.ImageSource = artist.profile_image;
            //lbl_name.Text = artist.name;
            //lbl_Title.Text = artist.name;
            //lbl_contactNumber.Text = artist.number.ToString();
            //lbl_subTitle.Text = artist.number.ToString();
            //lbl_commission.Text = artist.commission.ToString();
        }
        async protected override void OnAppearing()
        {

            await GetAllArtists();


        }
        public async Task GetAllArtists()
        {
            APIResponse _apiresponse = new APIResponse();
            List<ArtistsModel> _artists = new List<ArtistsModel>();
            _apiresponse = await App._dataManager.GetArtistsDetails();
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
                            _artists = _apiresponse.Data.ConvertAs<List<ArtistsModel>>();

                            if (_artists != null)
                            {

                                var artist = _artists.Where(x => x.id == _uid).FirstOrDefault();
                                lbl_address.Text = artist.address;
                                sfAvatar_profileImg.ImageSource = artist.profile_image;
                                lbl_name.Text = artist.name;
                                lbl_Title.Text = artist.username;
                                lbl_contactNumber.Text = artist.number.ToString();
                                lbl_subTitle.Text = artist.number.ToString();
                                lbl_commission.Text = artist.commission.ToString();
                                lbl_emergency.Text = artist.emergency_number.ToString();



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
       async private void OnClickBackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

       async private void edit_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditArtistPage(_uid));
        }

        private void Emer_Tapped(object sender, EventArgs e)
        {

        }

        private void Image_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(lbl_contactNumber.Text);
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