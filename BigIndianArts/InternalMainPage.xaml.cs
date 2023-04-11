using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using BigIndianArts.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InternalMainPage : ContentPage
    {
        private bool _textChanged;

        public ObservableCollection<MenuModel> Option_list { get; }

        public InternalMainPage()
        {
            InitializeComponent();
            if (App._Role == "Admin")
            {
                lbl_what.IsVisible = false;
                lbl_amount.IsVisible = false;
                lbl_nmae.Text = App._LoggedUserName;
                lbl_role.Text ="Manager";
                btn_createOrder.IsVisible = true;
                Option_list = new ObservableCollection<MenuModel>()
            {
                new MenuModel(){m_id="1",icon="order.png",name="Orders"},
                new MenuModel(){m_id="8",icon="status.png",name="Status"},
               
                new MenuModel(){m_id="3",icon="artist.png",name="Artist"},
                new MenuModel(){m_id="4",icon="gallery.png",name="Gallery"},
                    new MenuModel(){m_id="9",icon="salary.png",name="Income"},
                 new MenuModel(){m_id="2",icon="expenceg.png",name="Expense"},
                 
                new MenuModel(){m_id="7",icon="report.png",name="Reports"},
                new MenuModel(){m_id="10",icon="loupe.png",name="Search"},


            };
                _collection.ItemsSource = Option_list;
            }
            else
            {
                btn_createOrder.IsVisible = true;
                lbl_role.Text = "Artist";
                lbl_nmae.Text = App._ArtistName;
                Option_list = new ObservableCollection<MenuModel>()
            {
                new MenuModel(){m_id="5",icon="order.png",name="Orders"},
                new MenuModel(){m_id="6",icon="status.png",name="Completed"},
              


            };
                _collection.ItemsSource = Option_list;
            }

            sfAvatar_profileImg.ImageSource = App._LoggedUserImg;
        }

        async protected override void OnAppearing()
        {
            await NotificationCount();
            await CommissionAmount();
           // await GetAllProducts();


        }
        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<MenuModel> _menu = new List<MenuModel>();
            MenuPost _menuPost = new MenuPost();
            _menuPost.role = App._Role;
            _apiresponse = await App._dataManager.GetAllMenu(_menuPost);
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
                            _menu = _apiresponse.Data.ConvertAs<List<MenuModel>>();

                            if (_menu != null)
                            {
                                _collection.ItemsSource = _menu;
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

        async private void ImageNotifyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationPage());
        }

        async private void TapGestureProfileRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage());
        }

        private void OnclickSerchbutton(object sender, EventArgs e)
        {

        }

        private void OnFocusSearchButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnClearSearchClicked(object sender, EventArgs e)
        {

        }

        private void homemenutapped(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;

        }
        public async Task NotificationCount()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
            NotificationModel notification = new NotificationModel();
            if (App._Role == "Admin")
            {
                notification.type = "Admin";
                notification.id = Convert.ToInt32(App._LoggedPersonNumber);
                _apiresponse = await App._dataManager.GetNotification(notification);
            }
            else
            {
                notification.type = "Artist";
                notification.id = Convert.ToInt32(App._LoggedPersonNumber);
                _apiresponse = await App._dataManager.GetNotification(notification);
            }
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
                            lbl_notificationCount.Text = 0.ToString();
                            //DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _orders = _apiresponse.Data.ConvertAs<List<GetOrderModel>>();

                            if (_orders != null)
                            {
                                foreach (var _item in _orders)
                                {


                                }
                                int _filterItem = 0;
                                if (App._Role == "Admin")
                                {
                                     _filterItem = _orders.Where(x => x.admin_read == 0).Count();
                                }
                                else
                                {
                                    _filterItem = _orders.Where(x => x.artist_read == 0).Count();
                                }
                                if (_filterItem > 99)
                                {
                                    lbl_notificationCount.Text = "99+";
                                }
                                else
                                {
                                    lbl_notificationCount.Text = _filterItem.ToString();
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        lbl_notificationCount.Text = 0.ToString();
                        // DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        public async Task CommissionAmount()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetCommission> _commissionModel = new List<GetCommission>();
            NotificationModel notification = new NotificationModel();
            if (App._Role == "Admin")
            {
                notification.type = "Admin";
                notification.id = Convert.ToInt32(App._LoggedPersonNumber);
                _apiresponse = await App._dataManager.GetCommission(notification);
            }
            else
            {
                notification.type = "Artist";
                notification.id = Convert.ToInt32(App._LoggedPersonNumber);
                _apiresponse = await App._dataManager.GetCommission(notification);
            }
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
                            lbl_notificationCount.Text = 0.ToString();
                            //DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _commissionModel = _apiresponse.Data.ConvertAs<List<GetCommission>>();

                            if (_commissionModel != null)
                            {
                                //foreach (var _item in _commissionModel)
                                //{


                                //}
                                DateTime dateStart = DateTime.Now.AddDays(-30);
                                DateTime dateEnd = DateTime.Now.AddDays(1).AddTicks(-1);
                                decimal zero = 0;
                                var _tempItem = _commissionModel.Where(x => x.status == "Settled").ToList();
                                var _items= _tempItem.Where(s => s.createdOn.Date >= dateStart && s.createdOn.Date <= dateEnd).Sum(t => t.com_amount);
                                //var _filterItem = _commissionModel.Where(x => x.createdOn == DateTime.Now && );
                                lbl_amnt.Text = _items.ToString();
                                //lbl_notificationCount.Text = _filterItem.Count().ToString();
                            }
                            else
                            {
                                lbl_amnt.Text = "0";
                            }

                        }
                    }
                    else
                    {
                        lbl_notificationCount.Text = 0.ToString();
                        lbl_amnt.Text = "0";  // DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        async private void Onsearching(object sender, TextChangedEventArgs e)
        {
            if (_textChanged)
            {
                return;
            }

            _textChanged = true;
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                _textChanged = false;
                return;

            }

            var ProductSearch = new ProductSearchPopupPage(e.NewTextValue);

            await PopupNavigation.Instance.PushAsync(ProductSearch);
            _textChanged = false;
        }

        async private void ServiceListItem_Tapped(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as MenuModel;
            if (_selectedBtn.m_id == "1")
            {
                await Navigation.PushAsync(new ViewOrdersPage());
            }
            else if (_selectedBtn.m_id == "2")
            {
                await Navigation.PushAsync(new ViewExpencePage());
            }
            else if (_selectedBtn.m_id == "3")
            {
                await Navigation.PushAsync(new ViewArtistPage());
            }
            else if (_selectedBtn.m_id == "4")
            {
                await Navigation.PushAsync(new GalleryPage());
            }
            else if (_selectedBtn.m_id == "5")
            {
                await Navigation.PushAsync(new ArtistOrdersPage());
            }
            else if (_selectedBtn.m_id == "6")
            {
                await Navigation.PushAsync(new CompletedPage(null));
            }
            else if (_selectedBtn.m_id == "7")
            {
                await Navigation.PushAsync(new ReportMenuPage());
            } 
            else if (_selectedBtn.m_id == "8")
            {
                await Navigation.PushAsync(new CompletedTasks());
            }
            else if (_selectedBtn.m_id == "9")
            {
                await Navigation.PushAsync(new  ViewAdIncomePage());
            } 
            else if (_selectedBtn.m_id == "10")
            {
                await Navigation.PushAsync(new  SearchPage());
            }
        }

        async private void kart_clicked(object sender, EventArgs e)
        {

        }
        async private void user_clicked(object sender, EventArgs e)
        {

        }

        private void BannerImg_clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void WishList_Clicked(object sender, EventArgs e)
        {

        }

        async private void CreateOrder_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderRequestPage());
        }
    }
}