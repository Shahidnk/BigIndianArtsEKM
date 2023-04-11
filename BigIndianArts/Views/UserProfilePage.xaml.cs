using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BigIndianArts.Views.FlyOut.FlyoutShellPage;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        public ObservableCollection<HomeResponse> Option_list { get; }

        public UserProfilePage()
        {
            InitializeComponent();

        }
        async protected override void OnAppearing()
        {
            if (App._Role == "Admin")
            {
                sfAvatar_profileImg.ImageSource = App._LoggedUserImg;
                lbl_subTitle.Text = App._ph_no;
                lbl_Title.Text = App._LoggedUser.ToString();
                lbl_contactNumber.Text = App._ph_no;


               // lbl_address.Text = "Big Indian Arts,Ground Floor Lulu, Cochin";

                lbl_name.Text = App._LoggedUserName.ToString();

              
            }
            else
            {
                await GetAllArtists();
            }

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

                                var artist = _artists.Where(x => x.login_id == App._LoggedUserId.ToString()).FirstOrDefault();
                              //  lbl_address.Text = artist.address;
                                sfAvatar_profileImg.ImageSource = artist.profile_image;
                                lbl_name.Text = artist.name;
                                lbl_Title.Text = artist.name;
                                lbl_contactNumber.Text = artist.number.ToString();
                                lbl_subTitle.Text = artist.number.ToString();
                                //lbl_commission.Text = artist.commission.ToString();



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


    }
}