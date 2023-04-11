using Acr.UserDialogs;
using BigIndianArts.Data;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BigIndianArts.Data.MediaStorageClient;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditArtistPage : ContentPage
    {
        private string item;
        private string login_id;
        private string _uid;

        public string TextFile { get; private set; }
        public string LocFile { get; private set; }
        public EditArtistPage(string _id)
        {
            InitializeComponent();
            _uid = _id;
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
                                ent_address.Text = artist.address;
                                ent_upload.Text = Path.GetFileName(artist.profile_image);
                                ent_artistName.Text = artist.name;
                                ent_userName.Text = artist.username;
                                ent_number.Text = artist.number.ToString();

                                ent_commision.Text = artist.commission.ToString();
                                ent_Emergencynumber.Text = artist.emergency_number.ToString();
                                ent_password.Text = artist.password;
                                item = artist.profile_image;
                                login_id = artist.login_id;


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

        async private void submit_clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ent_userName.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please Enter UserName");
                ent_userName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_artistName.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please Enter Artist Name");
                ent_artistName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_password.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter password");
                ent_password.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_commision.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Commision");
                ent_commision.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_number.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter contact number");
                ent_number.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_address.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter address");
                ent_address.Focus();
                return;
            }
            if (!CheckNumber(ent_number.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid number");
                ent_number.Focus();
                return;
            }
            if (!CheckUserName(ent_userName.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid User Name");
                ent_userName.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(combo_experience.Text))
            //{
            //    DependencyService.Get<IAlertPlayer>().AlertMessege("Please select experience");
            //    combo_experience.Focus();
            //    return;
            //} 

            if (string.IsNullOrEmpty(item))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please upload Image");
                // combo_experience.Focus();
                return;
            }
            if (!CheckNumber(ent_number.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid number");
                ent_number.Focus();
                return;
            }
            if (!CheckNumber(ent_Emergencynumber.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid Emergency number");
                ent_Emergencynumber.Focus();
                return;
            }
            ArtistModel _orderData = new ArtistModel();
            APIResponse _apiResponce = new APIResponse();

            _orderData.id =Convert.ToInt32(login_id);
            _orderData.artist_name = ent_artistName.Text;
            _orderData.username = ent_userName.Text;
            _orderData.createdon = DateTime.Now;
            _orderData.password = ent_password.Text;
            _orderData.commision = Convert.ToInt32(ent_commision.Text);
            _orderData.contact_number = Convert.ToDecimal(ent_number.Text);
            _orderData.emergency = Convert.ToDecimal(ent_Emergencynumber.Text);
            _orderData.level = combo_experience.Text;
            _orderData.address = ent_address.Text;
            _orderData.image = item;

            _orderData.remarks = ent_remarks.Text;


            _apiResponce = await App._dataManager.EditArtist(_orderData);

            if (_apiResponce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please check your network connection or try again later.");

                return;
            }

            if (_apiResponce.IsSuccess == true)
            {
                if (_apiResponce.Data == null)
                {
                    DependencyService.Get<IAlertPlayer>().AlertMessege(_apiResponce.Message);
                    UserDialogs.Instance.HideLoading();

                    return;
                }

                var dat = _apiResponce.Data;


                DependencyService.Get<IAlertPlayer>().AlertMessege("Artist Updated Successfully");
                await Navigation.PopAsync();
            }
            else
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege(_apiResponce.Message);
            }

        }

        async private void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void ImageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    TextFile = $"File Name: {result.FileName}";
                    LocFile = result.FullPath;

                }
                await AddSignToSTorage(LocFile);


            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }


        }
        private async Task AddSignToSTorage(string fileData)
        {
            UserDialogs.Instance.ShowLoading("", MaskType.Black);
            APIResponse _apiResponce = new APIResponse();
            try
            {
                LoadingPage loadingPage = new LoadingPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);
                UserDialogs.Instance.ShowLoading("", MaskType.Black);

                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.ConstrainedInternet || current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {


                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "No network connection", "OK");

                    return;
                }

                StoredMediaAssets _addstoragerslt = new StoredMediaAssets();
                List<StoredMediaAssets> _mediaAssetRslt = new List<StoredMediaAssets>();
                MediaStorageClient _mediastorageclient = new MediaStorageClient();

                List<string> _files = new List<string>();
                _files.Add(fileData);

                _apiResponce = await _mediastorageclient.AddToStorage(_files);


                if (_apiResponce == null)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                    Application.Current.MainPage = new FlyoutPage();
                    return;
                }

                if (_apiResponce.IsSuccess != true)
                {


                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "API Responce Error", "OK");

                    return;
                }
                if (_apiResponce.Data == null)
                {


                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");


                    return;
                }

                _addstoragerslt = _apiResponce.Data.ConvertAs<StoredMediaAssets>();

                item = _addstoragerslt.Url;
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    ent_upload.Text = Path.GetFileName(item);


                }
                else
                {
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {

                if (PopupNavigation.PopupStack.Count > 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }

                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                Application.Current.MainPage = new FlyoutPage();

            }
        }
        bool CheckNumber(string strPhoneNumber)
        {
            string MatchPhoneNumberPattern = "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (strPhoneNumber != null)
                return Regex.IsMatch(strPhoneNumber, MatchPhoneNumberPattern);
            else
                return false;


        }
        bool CheckUserName(string strUserName)
        {
            string MatchUserNamePattern = @"^[a-zA-Z0-9_.-]*$";
            if (strUserName != null)
                return Regex.IsMatch(strUserName, MatchUserNamePattern);
            else
                return false;


        }
        private void ent_number_Unfocused(object sender, FocusEventArgs e)
        {
            if (!CheckNumber(ent_number.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid number");
                //ent_number.Focus();
                return;
            }
        }

        private void ent_userName_Unfocused(object sender, FocusEventArgs e)
        {
            if (!CheckUserName(ent_userName.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid User Name");
                //ent_userName.Focus();
                return;
            }
        }

        private void ent_Emergencynumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!CheckNumber(ent_Emergencynumber.Text))
            {

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid number");
                //ent_number.Focus();
                return;
            }
        }
    }
}