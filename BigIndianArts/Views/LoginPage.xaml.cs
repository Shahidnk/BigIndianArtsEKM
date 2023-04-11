using Acr.UserDialogs;
using BigIndianArts.CustomControls;
using BigIndianArts.Data;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using BigIndianArts.Views.FlyOut;
using Plugin.FirebasePushNotification;
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
    public partial class LoginPage : ContentPage
    {
        private bool _isLoginClicked;
        LoginModel _loginDetails = new LoginModel();
        APIResponse _login_responce = new APIResponse();
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }
        protected void Back(object s, EventArgs e)
        {
            // Navigation.PopAsync();
        }

        private async void Login(object sender, EventArgs e)
        {
            if (_isLoginClicked)
                return;

            _isLoginClicked = true;

            if ((string.IsNullOrEmpty(ent_userName.Text) || string.IsNullOrWhiteSpace(ent_userName.Text)) && (string.IsNullOrEmpty(ent_passWord.Text) || string.IsNullOrWhiteSpace(ent_passWord.Text)))
            {
                ent_userName.TextColor = Color.Red;
                ent_passWord.TextColor = Color.Red;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter username & password");
                _isLoginClicked = false;
                return;
            }

            if (string.IsNullOrEmpty(ent_userName.Text) || string.IsNullOrWhiteSpace(ent_userName.Text))
            {
                ent_userName.TextColor = Color.Red;

                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter username");
                _isLoginClicked = false;
                return;
            }

            if (string.IsNullOrEmpty(ent_passWord.Text) || string.IsNullOrWhiteSpace(ent_passWord.Text))
            {

                ent_passWord.TextColor = Color.Red;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter password");
                _isLoginClicked = false;
                return;
            }

            UserDialogs.Instance.ShowLoading("", MaskType.Black);

            App._isnetwrk = DependencyService.Get<IAlertPlayer>().IsNetworkAvailable();

            if (!App._isnetwrk)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please check your network connection or try again later.");
                _isLoginClicked = false;
                return;
            }


            var _username = ent_userName.Text;
            var _password = ent_passWord.Text;

            _loginDetails.email_id = _username;
            _loginDetails.password = _password;

            _login_responce = await App._dataManager.LoginTask(_loginDetails);

            if (_login_responce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please check your network connection or try again later.");
                _isLoginClicked = false;
                return;
            }

            if (_login_responce.IsSuccess == true)
            {
                if (_login_responce.Data == null)
                {
                    UserDialogs.Instance.HideLoading();
                    _isLoginClicked = false;
                    return;
                }

                EmployeeDetailsModel _employeDetails = _login_responce.Data.ConvertAs<EmployeeDetailsModel>();

                if (_employeDetails == null)
                {
                    UserDialogs.Instance.HideLoading();
                    _isLoginClicked = false;
                    return;
                }

                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                Application.Current.Properties["UserId"] = Convert.ToInt32(_login_responce.id);
                Application.Current.Properties["PersonNumber"] = _employeDetails.id;
                Application.Current.Properties["UserImg"] = _employeDetails.profile_image;
                Application.Current.Properties["UserName"] = _employeDetails.name;
                Application.Current.Properties["LUserName"] = _login_responce.username;
                Application.Current.Properties["ArtistName"] = _employeDetails.name;
                Application.Current.Properties["number"] = _employeDetails.number;
                Application.Current.Properties["Role"] = _login_responce.role;


                await Application.Current.SavePropertiesAsync();


                App._loggedUserDetails = _employeDetails;

                App._isLoggedIn = true;
                App._LoggedUserId = Convert.ToInt32(_login_responce.id);
                App._LoggedPersonNumber = _employeDetails.id;
                App._LoggedUserImg = _employeDetails.profile_image;
                App._LoggedUserName = _employeDetails.name;
                App._LoggedUser = _login_responce.username;
                App._Role = _login_responce.role;
                App._ArtistName = _employeDetails.name;
                App._ph_no = _employeDetails.number;
               
                App._dataManager = new DataManager();






                CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
                {
                    try
                    {
                        string _guid = Application.Current.Properties.ContainsKey("DeviceGuid") ? Application.Current.Properties["DeviceGuid"].ToString() : null;

                        if (App._isLoggedIn)
                        {

                            if (_guid == null)
                            {
                                Guid g = Guid.NewGuid();
                                _guid = g.ToString();
                                Application.Current.Properties["DeviceGuid"] = _guid;
                                await Application.Current.SavePropertiesAsync();
                            }
                            DeviceTokensModel _devicetoken = new DeviceTokensModel()
                            {
                                DeviceId = _guid,
                                Fcmtoken = p.Token,
                                Platform = Device.RuntimePlatform,
                                EmployeeId = App._LoggedUserId
                            };
                            APIResponse apiResponse = new APIResponse();
                            apiResponse = await App._dataManager.DeviceTokenSaveAsync(_devicetoken);
                            if (apiResponse == null)
                            {

                            }
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                };


                Application.Current.MainPage = new FlyoutShellPage();
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                ent_userName.TextColor = Color.Red;
                ent_passWord.TextColor = Color.Red;

                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter valid username and password");
            }


            UserDialogs.Instance.HideLoading();

            _isLoginClicked = false;
        }
    }
}