using Acr.UserDialogs;
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
    public partial class SettlementPage : ContentPage
    {
        string curMonth;
        private ArtistsModel _loadData;
        private IEnumerable<GetCommission> _items;

        public SettlementPage(ArtistsModel artists)
        {
            InitializeComponent();
            curMonth = DateTime.Now.ToString("MMMM");
            combo_month.Text = curMonth;
            _loadData = artists;
        }
        async protected override void OnAppearing()
        {
            await CommissionAmount(curMonth);
        }
        public async Task CommissionAmount(string monthc)
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetCommission> _commissionModel = new List<GetCommission>();
            NotificationModel notification = new NotificationModel();

            notification.type = "Admin";
            notification.id = Convert.ToInt32(App._LoggedPersonNumber);
            _apiresponse = await App._dataManager.GetCommission(notification);

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
                            _commissionModel = _apiresponse.Data.ConvertAs<List<GetCommission>>();

                            if (_commissionModel != null)
                            {
                                foreach (var _item in _commissionModel)
                                {
                                    var _crDate = Convert.ToDateTime(_item.createdOn);
                                    _item.date = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                    _item.month = _crDate.ToString("MMMM");



                                }
                                DateTime dateStart = DateTime.Now.AddDays(-30);
                                DateTime dateEnd = DateTime.Now.AddDays(1).AddTicks(-1);

                                decimal zero = 0;
                                var _tempItem = _commissionModel.Where(x => x.status == "Settled" && x.artist_id == _loadData.id).ToList();
                                _items = _tempItem.Where(s => s.month == monthc);
                                int totalcom = 0;
                                foreach (var _mitem in _items)
                                {
                                    if (_mitem.status == "Settled")
                                    {
                                        _mitem.status = "Pending";
                                    }
                                    else if (_mitem.status == "Done")
                                    {
                                        _mitem.status = "Settled";
                                    }
                                    totalcom = +Convert.ToInt32(_mitem.com_amount) + totalcom;
                                }
                                if (_items.Count() > 0)
                                {
                                    frm_reports.IsVisible = true;
                                    btn_settle.IsEnabled = true;
                                }
                                else
                                {
                                    frm_reports.IsVisible = false;
                                    btn_settle.IsEnabled = false;
                                }
                                BindableLayout.SetItemsSource(stk_reports, _items);
                                lbl_total.Text = totalcom.ToString();

                            }
                            else
                            {
                                // lbl_amnt.Text = "0";
                            }

                        }
                    }
                    else
                    {
                        //lbl_notificationCount.Text = 0.ToString();
                        //lbl_amnt.Text = "0";  // DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }

        private void ImageBackButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ReportBindable_Tapped(object sender, EventArgs e)
        {

        }

        async private void combo_month_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            curMonth = e.Value.ToString();
            await CommissionAmount(curMonth);
        }

        async private void Settle_clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Settlement", "Do you want to settle the amount ₹ " + lbl_total.Text, "Settle", "No");
           
            if (action)
            {
                ExpenceModel _expenceSave = new ExpenceModel();
                APIResponse _apiResponce = new APIResponse();
                DateTime _now = DateTime.Now;

                _expenceSave.amount = lbl_total.Text;

                _expenceSave.type = curMonth + " Month Settlement of " + _loadData.name;
                _expenceSave.createdOn = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);



                //_expenceModel.remarks = ent_remarks.Text;


                _apiResponce = await App._dataManager.SavecommissionExp(_expenceSave);

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
                        UserDialogs.Instance.HideLoading();

                        return;
                    }

                    var data = _apiResponce.Data;

                    foreach (var upItems in _items)
                    {
                        ExpenceModel _expenceModel = new ExpenceModel();


                        _expenceModel.id = upItems.id;




                        //_expenceModel.remarks = ent_remarks.Text;


                        _apiResponce = await App._dataManager.Settlecommission(_expenceModel);

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
                                UserDialogs.Instance.HideLoading();

                                return;
                            }

                            var dat = _apiResponce.Data;


                           
                        }
                    }
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Settlement Successfully");
                    await Navigation.PopAsync();
                }


               
            }
        }
    }
}