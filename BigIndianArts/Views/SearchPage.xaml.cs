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
    public partial class SearchPage : ContentPage
    {
        private bool _isSearchResetManually;
        private string _searchKeyword;
        private List<GetOrderModel> _tempItem;

        public SearchPage()
        {
            InitializeComponent();
        }
        private void SearchIconClicked(object sender, EventArgs e)
        {
            stk_navbarTitleSection.IsVisible = false;
            stk_searchBox.IsVisible = true;

            //await Task.Delay(100);
            ent_search.Focus();
        }
        private async void CloseSearchBox_Clicked(object sender, EventArgs e)
        {
            _isSearchResetManually = true;
            if (!string.IsNullOrEmpty(ent_search.Text))
            {
                _isSearchResetManually = false;

            }
            ent_search.Text = _searchKeyword= "";
            stk_searchBox.IsVisible = false;
            stk_navbarTitleSection.IsVisible = true;
            await GetAllProducts();
        }
        private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isSearchResetManually)
            {
                _searchKeyword = ent_search.Text;
                cv_orders.ItemsSource = null;
                cv_orders.ItemsSource = _tempItem.Where(x => x.contact_number.Contains(_searchKeyword)|| x.bill_number.Contains(_searchKeyword)).ToList(); ;
                if (string.IsNullOrEmpty(_searchKeyword))
                {
                    await GetAllProducts();
                }
            }
        }
        private async void ClearSearch_Clicked(object sender, EventArgs e)
        {
            _isSearchResetManually = true;
            if (!string.IsNullOrEmpty(ent_search.Text))
            {
                _isSearchResetManually = false;

            }
            ent_search.Text = "";
            _searchKeyword = "";

            await GetAllProducts();
        }
        private void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            _isSearchResetManually = true;
        }
        private void SearchEntry_UnFocused(object sender, FocusEventArgs e)
        {
            _isSearchResetManually = false;
        }

        private void ImgBackBtn_Clicked(object sender, EventArgs e)
        {

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
                            cv_orders.ItemsSource = null;
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
                                    if (_item.frame == "yes")
                                    {
                                        _item.frame = "With Frame";
                                    }
                                    else
                                    {
                                        _item.frame = "No Frame";
                                    }

                                    _item.balance = (Convert.ToDecimal(_item.price) - Convert.ToDecimal(_item.advance)).ToString();
                                }

                                //if (_key == "")
                                //{
                                //    cv_orders.ItemsSource = _orders;
                                //}
                                //else
                                //{
                                //    cv_orders.ItemsSource = null;
                                //    cv_orders.ItemsSource = _orders.Where(x=>x.contact_number.Contains(_key)).ToList();
                                //}

                                //cv_orders.ItemsSource = _orders;
                                cv_orders.ItemsSource = null;
                                _tempItem = _orders;
                            }
                        }
                    }
                    else
                    {
                        cv_orders.ItemsSource = null;
                        //DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        async private void Cv_selectionChanged(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as GetOrderModel;
            await Navigation.PushAsync(new OrderDetailsPage(_selectedBtn.id, App._Role));

        }

        async private void CreateOrder_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderRequestPage());
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}