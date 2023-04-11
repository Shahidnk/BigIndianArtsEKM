using BigIndianArts.Data;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using BigIndianArts.Views;
using BigIndianArts.Views.FlyOut;
using BigIndianArts.Views.OnBoarding;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;

[assembly: ExportFont("ProximaNova-Regular.otf", Alias = "ThemeFontRegular")]
[assembly: ExportFont("ProximaNova-Regular.otf", Alias = "ThemeFontMedium")]
[assembly: ExportFont("Proxima Nova-Bold.otf", Alias = "ThemeFontBold")]

[assembly: ExportFont("UlmGroteskBold.ttf", Alias = "UlmGroteskBold")]
[assembly: ExportFont("UlmGroteskMedium.ttf", Alias = "UlmGroteskMedium")]
[assembly: ExportFont("UlmGroteskRegular.ttf", Alias = "UlmGroteskRegular")]
[assembly: ExportFont("UlmGroteskExtrabold.ttf", Alias = "UlmGroteskExtrabold")]
[assembly: ExportFont("UlmGroteskLight.ttf", Alias = "UlmGroteskLight")]
[assembly: ExportFont("UlmGrotesk.ttf", Alias = "UlmGrotesk")]
[assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace BigIndianArts
{
    public partial class App : Application
    {
        public static readonly string BaseURL = "http://bigindianarts.ml/api_new";
        private bool _IsNotificationOpened;

        public static DataManager _dataManager { get; set; }
        public static bool _isnetwrk { get; set; }
        public static bool _isrunningNoNetworkPage { get; set; }
        public static bool _isLoggedIn { get; set; }
        public static bool _IsOnscreenViewed { get; set; }
        public static bool _IsConversationLoadingFirstTime { get; set; }
        public static int _LoggedUserId { get; set; }
        public static string _LoggedPersonNumber { get; set; }
        public static string _ph_no { get; set; }
        public static string _LoggedUserImg { get; set; }
        public static string _LoggedUserName { get; set; }
        public static string _LoggedUser { get; set; }
        public static string _Role { get; set; }
        public static string _ArtistName { get; set; }
        public static string _UserName { get; set; }
        public static bool _IsFromNotification { get; set; }
        public static EmployeeDetailsModel _loggedUserDetails { get; internal set; }
        public string FireToken { get; set; }

        public App()
        {
            InitializeComponent();

            _isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;


            _IsOnscreenViewed = Current.Properties.ContainsKey("IsOnscreenViewed") ? Convert.ToBoolean(Current.Properties["IsOnscreenViewed"]) : false;
            _IsConversationLoadingFirstTime = Current.Properties.ContainsKey("IsConversationLoadingFirstTime")
                ? (Convert.ToBoolean(Current.Properties["IsConversationLoadingFirstTime"]) ? true : false) : true;

            if (_isLoggedIn)
            {
                _LoggedUserId = (int)(Current.Properties.ContainsKey("UserId") ? Current.Properties["UserId"] : 0);
                _LoggedPersonNumber = (string)(Current.Properties.ContainsKey("PersonNumber") ? Current.Properties["PersonNumber"] : null);
                _LoggedUserImg = (string)(Current.Properties.ContainsKey("UserImg") ? Current.Properties["UserImg"] : null);
                _UserName = (string)(Current.Properties.ContainsKey("UserName") ? Current.Properties["UserName"] : null);
                _ArtistName = (string)(Current.Properties.ContainsKey("ArtistName") ? Current.Properties["ArtistName"] : null);
                _Role = (string)(Current.Properties.ContainsKey("Role") ? Current.Properties["Role"] : null);

                _LoggedUserName = (string)(Current.Properties.ContainsKey("UserName") ? Current.Properties["UserName"] : null);
                _LoggedUser = (string)(Current.Properties.ContainsKey("LUserName") ? Current.Properties["LUserName"] : null);
                _ph_no = (string)(Current.Properties.ContainsKey("number") ? Current.Properties["number"] : null);
                FireToken = (string)(Current.Properties.ContainsKey("FireToken") ? Current.Properties["FireToken"] : null);

                _dataManager = new DataManager();

                if (_LoggedUserId == 0)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        MainPage = new NavigationPage(new LoginPage())
                        {
                            BarBackgroundColor = Color.FromHex("#238823"),
                            BarTextColor = Color.White
                        };
                    }
                    else
                    {

                        MainPage = new NavigationPage(new LoginPage());


                    }
                }
                else
                {


                    if (!_IsFromNotification)
                        MainPage = new FlyoutShellPage();

                }
            }
            else
            {
                _dataManager = new DataManager();

                //if (_IsOnscreenViewed)
                //{
                if (Device.RuntimePlatform == Device.Android)
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }

                //}
                //else
                //{
                //    Device.BeginInvokeOnMainThread(async () =>
                //    {
                //        Current.Properties["IsOnscreenViewed"] = Boolean.TrueString;
                //        await Current.SavePropertiesAsync();
                //    });

                //    MainPage = new NavigationPage(new OnBoardingAnimationPage());

                //}


            }
            //if (string.IsNullOrEmpty(FireToken))
            //{
            //    CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
            //}
        }

        //private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        //{
        //    Current.Properties["FireToken"] = e.Token;
        //    FireToken = (string)(Current.Properties.ContainsKey("FireToken") ? Current.Properties["FireToken"] : null);
        //    //System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
        //}

        public static async Task<bool> GetNetworkStatus()
        {
            _isnetwrk = DependencyService.Get<IAlertPlayer>().IsNetworkAvailable();

            return _isnetwrk;
        }

        public static async Task NetworkCheck(Page _page, bool _fromPage)
        {
            try
            {
                _isnetwrk = DependencyService.Get<IAlertPlayer>().IsNetworkAvailable();

                if (!_fromPage)
                {
                    if (_isnetwrk == false)
                    {
                        if (_page == null)
                        {
                            if (!_isrunningNoNetworkPage)
                            {
                                _isrunningNoNetworkPage = true;
                                Current.MainPage = new NoNetworkPage();
                            }
                        }

                        FlyoutShellPage _page1 = (FlyoutShellPage)Current.MainPage;
                        await Task.Delay(100);

                        if (!_isrunningNoNetworkPage)
                        {
                            _isrunningNoNetworkPage = true;
                            await (_page1.Items as NavigationPage).PushAsync(new NoNetworkPage());
                        }
                    }
                }
                else
                {
                    if (_isnetwrk == false)
                    {
                        await Task.Delay(100);

                        if (!_isrunningNoNetworkPage)
                        {
                            _isrunningNoNetworkPage = true;
                            await _page.Navigation.PushAsync(new NoNetworkPage());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnStart()
        {
            AppCenter.Start("android={b91ff937-8a26-4a78-a582-749d799fa379};",
                     typeof(Analytics), typeof(Crashes));



            CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
            {
                try
                {
                    string _guid = (string)(Current.Properties.ContainsKey("DeviceGuid") ? Current.Properties["DeviceGuid"] : null);
                    

                      if (_isLoggedIn)
                    {

                        if (_guid == null)
                        {
                            Guid g = Guid.NewGuid();
                            _guid = g.ToString();
                            Current.Properties["DeviceGuid"] = _guid;
                            await Current.SavePropertiesAsync();
                        }
                        DeviceTokensModel _devicetoken = new DeviceTokensModel()
                        {
                            DeviceId = _guid,
                            Fcmtoken = p.Token,
                            Platform = Device.RuntimePlatform,
                            CreatedOn = DateTime.Now.ToString(),
                            EmployeeId = _LoggedUserId
                        };
                        APIResponse apiResponse = new APIResponse();
                        apiResponse = await _dataManager.DeviceTokenSaveAsync(_devicetoken);
                        if (apiResponse == null)
                        {

                        }
                    }
                }
                catch (Exception exc)
                {

                }
            };


            //CrossFirebasePushNotification.Current.OnNotificationOpened += async (s, p) =>
            //{
            //    //  MainPage= new NavigationPage(new LeaveBalancePage());
            //    if (_IsNotificationOpened)
            //        return;
            //    _IsNotificationOpened = true;
            //    _IsFromNotification = true;

            //    try
            //    {
            //        if (p.Data.Count > 1)
            //        {
            //            NavigationPage _page = (NavigationPage)MainPage;
            //            var nav = MainPage.Navigation;

            //            await nav.PopToRootAsync(true);
            //            //FlyoutShellPage _page = (FlyoutShellPage)Current.MainPage;
            //            //var nav = MainPage.Navigation;

            //            await nav.PopToRootAsync(true);
            //            // await Task.Delay(100);
            //            //int _count = Application.Current.MainPage.Navigation.NavigationStack.Count;
            //            int targetId = 0;
            //            string targetUser = "";

            //            foreach (var data2 in p.Data)
            //            {
            //                if (data2.Key == "targetId")
            //                {
            //                    try
            //                    {
            //                        targetId = int.Parse(data2.Value.ToString());
            //                    }
            //                    catch (Exception)
            //                    {
            //                        targetId = 0;
            //                    }

            //                }
            //                if (data2.Key == "userType")
            //                {
            //                    try
            //                    {
            //                        targetUser = data2.Value.ToString();
            //                    }
            //                    catch (Exception)
            //                    {
            //                        targetId = 0;
            //                    }

            //                }
            //                if (data2.Key == "targetPage")
            //                {

            //                    string _name = data2.Value.ToString();
            //                    //string _name = GetModule(data2.Value.ToString());
            //                    switch (_name)
            //                    {
            //                        case "Artist Order":
                                       
            //                                await nav.PushAsync(new ArtistOrderDetailsPage(targetId.ToString()));

                                       
                                      
            //                            break;

                                   

            //                    }
            //                    _IsNotificationOpened = false;
            //                    break;
            //                }


            //            }
            //            _page = null;
            //            _IsNotificationOpened = false;
            //            _IsFromNotification = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //};

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {

            };

            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Dismissed");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
