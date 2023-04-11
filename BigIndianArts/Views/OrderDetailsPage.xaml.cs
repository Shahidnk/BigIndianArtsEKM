using Acr.UserDialogs;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsPage : ContentPage
    {
        private string _imageUrl;
        private string _finalimageUrl;
        private GetOrderModel _loadData;
        private string _loadID;

        public OrderDetailsPage(string _id, string _type)
        {
            InitializeComponent();
            _loadID = _id;
            //  lbl_advanceAmount.Text = getOrder.advance;
            // lbl_BalanceAmount.Text = (Convert.ToInt32(getOrder.price) - Convert.ToInt32(getOrder.advance)).ToString();

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
                                    //if (_item.frame == "yes")
                                    //{
                                    //    _item.frame = "With Frame";
                                    //}
                                    //else
                                    //{
                                    //    _item.frame = "No Frame";
                                    //}

                                    _item.balance = (Convert.ToDecimal(_item.price) - Convert.ToDecimal(_item.advance)).ToString();
                                }
                                _loadData = _orders.Where(x => x.id == _loadID).FirstOrDefault();


                                lbl_orderAmount.Text = _loadData.price;
                                lbl_BillNumber.Text = _loadData.bill_number;
                                lbl_advanceAmount.Text = _loadData.advance;
                                lbl_balanceAmount.Text = (Convert.ToInt32(_loadData.price) - Convert.ToInt32(_loadData.advance)).ToString();
                                ent_amount.Text = (Convert.ToInt32(_loadData.price) - Convert.ToInt32(_loadData.advance)).ToString();
                                lbl_CustomerName.Text = _loadData.customer_name;
                                lbl_artist.Text = _loadData.assign_name;
                                lbl_createdDate.Text = _loadData.createdOn;
                                lbl_CustomerNumber.Text = _loadData.contact_number;
                                lbl_NumberFace.Text = _loadData.people_no;
                                lbl_orderID.Text = _loadData.id;
                                lbl_orientation.Text = _loadData.orientation;
                                lbl_remarks.Text = _loadData.remarks;
                                lbl_type.Text = _loadData.type;
                                lbl_status.Text = _loadData.status;
                                lbl_expectedDate.Text = _loadData.expected_date;
                                lbl_Size.Text = _loadData.size;
                                lbl_courierService.Text = Path.GetFileName(_loadData.uploaded_image);
                                lbl_commision.Text = _loadData.commission;
                                lbl_DeliveryMode.Text = _loadData.delivery_mode.TrimStart();
                                lbl_Frame.Text = _loadData.frame;
                                lbl_mode.Text = _loadData.paymentmode;
                                if (_loadData.status == "Waiting For Approval")
                                {
                                    Stk_MoreActionSection.IsVisible = true;
                                }
                                if (_loadData.isVideo == "True")
                                {
                                    lbl_isVideo.Text = "Yes";
                                }
                                else
                                {
                                    lbl_isVideo.Text = "No";
                                }
                                if (_loadData.online_order == "True")
                                {
                                    lbl_isOnline.Text = "Yes";
                                }
                                else
                                {
                                    lbl_isOnline.Text = "No";
                                }
                                if (_loadData.status == "Completed")
                                {
                                    btn_edit.IsVisible = false;
                                    btn_delete.IsVisible = false;
                                    bx_completed.IsVisible = true;
                                    lbl_Completed.IsVisible = true;
                                    lbl_completedTitle.IsVisible = true;
                                }
                                else if (_loadData.status == "Waiting For Approval")
                                {
                                    bx_completed.IsVisible = true;
                                    lbl_Completed.IsVisible = true;
                                    lbl_completedTitle.IsVisible = true;

                                }
                                else
                                {
                                    btn_edit.IsVisible = true;
                                    btn_delete.IsVisible = true;
                                    bx_completed.IsVisible = false;
                                    lbl_Completed.IsVisible = false;
                                    lbl_completedTitle.IsVisible = false;
                                }

                                if (_loadData.notificationStatus == 1)//Settle action visibilty
                                {
                                    lbl_settle.IsVisible = false;
                                    bx_settle.IsVisible = false;
                                    stk_settle.IsVisible = false;
                                    ent_amount.IsVisible = false;
                                    lbl_payMethod.IsVisible = false;
                                    bx_payMethod.IsVisible = false;
                                    combo_payMethod.IsVisible = false;

                                }
                                else
                                {
                                    lbl_settle.IsVisible = true;
                                    bx_settle.IsVisible = true;
                                    stk_settle.IsVisible = true;
                                    lbl_payMethod.IsVisible = true;
                                    bx_payMethod.IsVisible = true;
                                    combo_payMethod.IsVisible = true;
                                }
                                lbl_Completed.Text = Path.GetFileName(_loadData.final_image);

                                _imageUrl = _loadData.uploaded_image;
                                _finalimageUrl = _loadData.final_image;
                            }
                        }
                    }
                    else
                    {

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
        private void timelineListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Image_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(_imageUrl, BrowserLaunchMode.SystemPreferred);
        }

        private void Combo_status_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        async private void CompletedImage_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(_finalimageUrl, BrowserLaunchMode.SystemPreferred);
        }

        async private void BtnAction_clicked(object sender, EventArgs e)
        {
            await UpdateOrder(Convert.ToInt32(_loadData.id), "Pending", "Rejected");
        }

        public async Task UpdateOrder(int _id, string _status, string action)
        {
            APIResponse _apiResponce = new APIResponse();
            UpdateAction _orderData = new UpdateAction();

            DateTime _now = DateTime.Now;
            _orderData.id = _id;
            _orderData.status = _status;
            _orderData.action = action;
            _orderData.title = _loadData.type;
            _orderData.user_id = _loadData.assign_id;
            _orderData.assign_empid = _loadData.assign_empid;
            _orderData.createdOn = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);



            _apiResponce = await App._dataManager.UpdateOrderAction(_orderData);

            if (_apiResponce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Data Null");

                return;
            }

            if (_apiResponce.IsSuccess == true)
            {
                if (_apiResponce.Data == null)
                {
                    UserDialogs.Instance.HideLoading();

                    return;
                }

                var dat = _apiResponce.Data;

                DependencyService.Get<IAlertPlayer>().AlertMessege("Order Updated Successfully.");

                await Navigation.PopAsync();
            }
        }

        async private void BtnActionSApprove_clicked(object sender, EventArgs e)
        {
            await UpdateOrder(Convert.ToInt32(_loadData.id), "Completed", "Approved");
        }

        async private void btn_edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditOrderRequestPage(_loadData, App._Role));
        }

        async private void Settle_clicked(object sender, EventArgs e)
        {
            APIResponse _apiResponce = new APIResponse();
            UpdateOrderModel _orderData = new UpdateOrderModel();

            DateTime _now = DateTime.Now;
            _orderData.id = Convert.ToInt32(_loadData.id);


            _orderData.user_id = _loadData.assign_id;
            _orderData.amount = ent_amount.Text;
            _orderData.amountadv = (Convert.ToDecimal(ent_amount.Text) + Convert.ToDecimal(_loadData.advance)).ToString();
            _orderData.createdOn = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);
            _orderData.paymentmode = combo_payMethod.Text;


            _apiResponce = await App._dataManager.UpdateOrderSettlements(_orderData);

            if (_apiResponce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Data Null");

                return;
            }

            if (_apiResponce.IsSuccess == true)
            {
                if (_apiResponce.Data == null)
                {
                    UserDialogs.Instance.HideLoading();

                    return;
                }
                lbl_settle.IsVisible = false;
                bx_settle.IsVisible = false;
                stk_settle.IsVisible = false;
                ent_amount.IsVisible = false;
                var dat = _apiResponce.Data;

                DependencyService.Get<IAlertPlayer>().AlertMessege("Order Settled Successfully.");

                await Navigation.PopAsync();
            }
        }

        async private void btn_delete_Clicked(object sender, EventArgs e)
        {


            var _status = await DisplayAlert("Delete", "Do you want delete this order?", "Delete", "Cancel");
            if (_status)
            {
                APIResponse _apiResponce = new APIResponse();
                DeleteOrder _orderData = new DeleteOrder();

                DateTime _now = DateTime.Now;
                _orderData.id = Convert.ToInt32(_loadData.id);






                _apiResponce = await App._dataManager.DeleteOrder(_orderData);

                if (_apiResponce == null)
                {
                    UserDialogs.Instance.HideLoading();
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Data Null");

                    return;
                }

                if (_apiResponce.IsSuccess == true)
                {


                    var dat = _apiResponce.Data;

                    DependencyService.Get<IAlertPlayer>().AlertMessege("Order Deleted Successfully.");

                    await Navigation.PopAsync();
                }
            }

        }
    }
}