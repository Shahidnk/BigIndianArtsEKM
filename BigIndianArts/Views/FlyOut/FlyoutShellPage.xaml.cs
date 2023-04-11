using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views.FlyOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutShellPage : Shell
    {
        public ObservableCollection<HomeResponse> Option_list { get; set; }
        public FlyoutShellPage()
        {
            InitializeComponent();
            Option_list = new ObservableCollection<HomeResponse>()
            {
                new HomeResponse(){_id="1",image="homefly.png",name="Home"},
                //new HomeResponse(){_id="2",image="category.png",name="All Categories"},
               // new HomeResponse(){_id="3",image="tshirt.png",name="My Orders"},
               // new HomeResponse(){_id="4",image="cartfly.png",name="My Cart"},
               // new HomeResponse(){_id="5",image="wishlist.png",name="My Wishlist"},
               // new HomeResponse(){_id="6",image="userfly.png",name="My Account"},
               // new HomeResponse(){_id="7",image="notification.png",name="My Notifications"},
                //new HomeResponse(){_id="8",image="note.png",name="Privacy Policy"},
                new HomeResponse(){_id="9",image="exit.png",name="Logout"},


            };
            MenuItemsListView.ItemsSource = Option_list;



        }

        async private void NewMenuItem_Tapped(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var option_selected = _tapped.Parameter as HomeResponse;
            switch (option_selected._id)
            {
                case "1":
                    {
                        await Navigation.PushAsync(new InternalMainPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "2":
                    {
                        await Navigation.PushAsync(new AllCategoryPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "3":
                    {
                        await Navigation.PushAsync(new ArtistOrdersPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "4":
                    {
                        await Navigation.PushAsync(new CartPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "5":
                    {
                        await Navigation.PushAsync(new WishListPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "6":
                    {
                        await Navigation.PushAsync(new UserProfilePage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "7":
                    {
                        await Navigation.PushAsync(new NotificationPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    } 
                case "8":
                    {
                        await Navigation.PushAsync(new PrivacyPolicyPage());
                        Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
                case "9":
                    {
                        await Task.Delay(200);

                        if (!await DisplayAlert("", "Are you sure you want to logout?", "Yes", "No"))
                        {
                           // _isMenuItemTapped = false;
                            return;
                        }


                      


                        Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
                        Application.Current.Properties["IsOnscreenViewed"] = Boolean.FalseString;
                        Application.Current.Properties["IsConversationLoadingFirstTime"] = Boolean.FalseString;
                        Application.Current.Properties["UserId"] = null;
                        Application.Current.Properties["PersonNumber"] = null;
                        Application.Current.Properties["UserImg"] = null;
                        Application.Current.Properties["UserName"] = null;
                        Application.Current.Properties["Role"] = null;
                        Application.Current.Properties["ArtistName"] = null;
                        Application.Current.Properties["DeviceGuid"] = null;

                       

                        await Application.Current.SavePropertiesAsync();

                        App._LoggedUserImg = null;
                        App._LoggedUserName = null;
                        App._IsOnscreenViewed = false;
                        App._isLoggedIn = false;
                        App._LoggedUserId = 0;
                        App._LoggedPersonNumber = null;
                        App._loggedUserDetails = null;
                        App._Role = null;
                        App._ArtistName = null;
                       


                        if (Device.RuntimePlatform == Device.Android)
                        {
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            //{
                            //    BarBackgroundColor = Color.FromHex("#238823"),
                            //    BarTextColor = Color.White
                            //};
                        }
                       
                        //Shell.Current.FlyoutIsPresented = false;
                        break;
                    }
            }
        }
        public class HomeResponse
        {
            public string _id { get; set; }
            public string image { get; set; }
            public string name { get; set; }
        }
    }
}