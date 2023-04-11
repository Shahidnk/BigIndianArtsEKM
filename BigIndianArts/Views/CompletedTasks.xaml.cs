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
    public partial class CompletedTasks : ContentPage
    {
        private List<GetOrderModel> _tempData;
        private List<GetOrderModel> changeDate;

        public CompletedTasks()
        {
            InitializeComponent();
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
                            cv_Pendings.ItemsSource = null;
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
                                _tempData = _orders;
                                cv_Pendings.ItemsSource = _orders.Where(x => x.status == "Waiting For Approval").ToList();
                            }
                        }
                    }
                    else
                    {
                        // cv_orders.ItemsSource = null;
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
        private void SfTabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            try
            {
                changeDate = null;
                changeDate = _tempData;


                if (e.Index == 1)
                {
                    cv_Approved.ItemsSource = changeDate.Where(x => x.status == "Completed" && x.request_status == "Approved").ToList();
                    selection_indctr.Color = (Color)Application.Current.Resources["ApprovedColor"];
                    Lbl_approved.TextColor = (Color)Application.Current.Resources["ApprovedColor"];
                    Lbl_rejected.TextColor = (Color)Application.Current.Resources["TitleTextColor"];
                    Lbl_pending.TextColor = (Color)Application.Current.Resources["TitleTextColor"];

                }
                else if (e.Index == 2)
                {
                    cv_Rejected.ItemsSource = changeDate.Where(x => x.status == "Pending" && x.request_status == "Rejected").ToList();
                    selection_indctr.Color = (Color)Application.Current.Resources["RejectedColor"];
                    Lbl_rejected.TextColor = (Color)Application.Current.Resources["RejectedColor"];
                    Lbl_pending.TextColor = (Color)Application.Current.Resources["TitleTextColor"];

                    Lbl_approved.TextColor = (Color)Application.Current.Resources["TitleTextColor"];

                }
                else if (e.Index == 0)
                {
                    //  cv_Pendings.ItemsSource = changeDate.Where(x => x.status == "Waiting For Approval" &&( x.request_status == "Pending"|| x.request_status == "Rejected")).ToList();
                    cv_Pendings.ItemsSource = changeDate.Where(x => x.status == "Waiting For Approval").ToList();

                    selection_indctr.Color = (Color)Application.Current.Resources["PendingColor"];
                    Lbl_pending.TextColor = (Color)Application.Current.Resources["PendingColor"];

                    Lbl_approved.TextColor = (Color)Application.Current.Resources["TitleTextColor"];
                    Lbl_rejected.TextColor = (Color)Application.Current.Resources["TitleTextColor"];


                }
            }
            catch (Exception ex)
            {

            }

        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}