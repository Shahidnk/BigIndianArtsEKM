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
    public partial class MonthlyReport : ContentPage
    {
        private List<GetExpenceModel> upExpences;
        private string curMonth;
        private string curYear;
        List<string> years = new List<string>();
        public MonthlyReport()
        {
            InitializeComponent();
            curMonth = DateTime.Now.ToString("MMMM");
            curYear = DateTime.Now.ToString("yyyy");
            combo_month.Text = curMonth;
           List<string> curr = new List<string>();
            for(int i = 2022; i <= DateTime.Now.Year; i++)
            {
                curr.Add(i.ToString());
            }
            combo_year.DataSource = curr;
            combo_year.Text = curYear;
        }
        public List<IncomeModel> UpIncomes { get; private set; }
        async protected override void OnAppearing()
        {
            //await GetAllIncome(curMonth, curYear);
        }


        public async Task GetAllExpences(string _month, string _year)
        {
            List<DailyReportModel> dailyReport = new List<DailyReportModel>();
            UserDialogs.Instance.ShowLoading("", MaskType.Black);
            APIResponse _apiresponse = new APIResponse();
            List<GetExpenceModel> _expenceModels = new List<GetExpenceModel>();
            _apiresponse = await App._dataManager.GetAllExpences();
            try
            {
                if (_apiresponse == null)
                {
                    UserDialogs.Instance.HideLoading();
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    return;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            UserDialogs.Instance.HideLoading();
                            DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                            return;
                        }
                        else
                        {
                            _expenceModels = _apiresponse.Data.ConvertAs<List<GetExpenceModel>>();

                            if (_expenceModels != null)
                            {
                                foreach (var _item in _expenceModels)
                                {

                                    if (!string.IsNullOrEmpty(_item.expence_date))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.expence_date);
                                        _item.expence_date = _crDate.ToString("dd MMM yyyy");
                                        _item.expence_date = _crDate.ToString("dd MMM yyyy");
                                        _item.month = _crDate.ToString("MMMM");
                                        _item.year = _crDate.ToString("yyyy");
                                    }

                                }
                                //  var _date = date_select.Date.ToString("dd MMM yyyy");
                                upExpences = new List<GetExpenceModel>();
                                upExpences = _expenceModels.Where(x => x.month == _month && x.year == _year).ToList();
                                List<GetExpenceIncomeModel> _expenceIncomeModels = new List<GetExpenceIncomeModel>();


                                if (upExpences != null)
                                {
                                    foreach (var _expItem in upExpences)
                                    {
                                        if (_expItem.Mode == "Expense")
                                        {
                                            _expenceIncomeModels.Add(new GetExpenceIncomeModel()
                                            {
                                                expence_date = _expItem.expence_date,
                                                expense_amount = _expItem.amount,
                                                Exid = _expItem.id,
                                                exptype = _expItem.type,
                                                paymentmode = _expItem.paymentmode

                                            });
                                        }
                                        else
                                        {
                                            _expenceIncomeModels.Add(new GetExpenceIncomeModel()
                                            {

                                                intype = _expItem.type,
                                                Inid = _expItem.id,
                                                income_amount = _expItem.amount,
                                                income_date = _expItem.expence_date,
                                                paymentmode = _expItem.paymentmode
                                            });
                                        }
                                    }
                                }
                                if (UpIncomes != null)
                                {
                                    foreach (var _InItem in UpIncomes)
                                    {

                                        _expenceIncomeModels.Add(new GetExpenceIncomeModel()
                                        {

                                            intype = _InItem.type,
                                            Inid = _InItem.order_id,
                                            income_amount = _InItem.amount,
                                            income_date = _InItem.date,
                                            paymentmode = _InItem.paymentmode

                                        });

                                    }
                                }
                               
                                int totalExp = 0;
                                int totalincome = 0;
                                foreach (var _item in _expenceIncomeModels)
                                {

                                    totalExp = +Convert.ToInt32(_item.expense_amount) + totalExp;
                                    totalincome = +Convert.ToInt32(_item.income_amount) + totalincome;
                                    if (!string.IsNullOrEmpty(_item.Inid))
                                    {
                                        dailyReport.Add(new DailyReportModel()
                                        {
                                            icon = "rupee.png",
                                            amount = _item.income_amount,
                                            date = _item.income_date,
                                            type = "Income",
                                            title = _item.intype,
                                            paymentmode = _item.paymentmode

                                        });
                                    }
                                    if (!string.IsNullOrEmpty(_item.Exid))
                                    {
                                        dailyReport.Add(new DailyReportModel()
                                        {
                                            icon = "decrease.png",
                                            amount = _item.expense_amount,
                                            date = _item.expence_date,
                                            type = "Expense",
                                            title = _item.exptype,
                                            paymentmode = _item.paymentmode

                                        });
                                    }
                                }
                                if (dailyReport.Count > 0)
                                {
                                    frm_reports.IsVisible = true;
                                }
                                else
                                {
                                    frm_reports.IsVisible = false;
                                }
                                lbl_expendse.Text = totalExp.ToString();
                                lbl_Income.Text = totalincome.ToString();
                                lbl_balance.Text = (totalincome - totalExp - (6000 * 31)).ToString();
                                BindableLayout.SetItemsSource(stk_reports, dailyReport);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                List<GetExpenceIncomeModel> _expenceIncomeModels = new List<GetExpenceIncomeModel>();
                                if (UpIncomes != null)
                                {


                                    foreach (var _InItem in UpIncomes)
                                    {

                                        _expenceIncomeModels.Add(new GetExpenceIncomeModel()
                                        {

                                            intype = _InItem.type,
                                            Inid = _InItem.order_id,
                                            income_amount = _InItem.amount,
                                            income_date = _InItem.date,
                                            paymentmode = _InItem.paymentmode
                                        });

                                    }
                                }
                                
                                int totalExp = 0;
                                int totalincome = 0;
                                foreach (var _item in _expenceIncomeModels)
                                {

                                    totalExp = +Convert.ToInt32(_item.expense_amount) + totalExp;
                                    totalincome = +Convert.ToInt32(_item.income_amount) + totalincome;
                                    if (!string.IsNullOrEmpty(_item.Inid))
                                    {
                                        dailyReport.Add(new DailyReportModel()
                                        {
                                            icon = "rupee.png",
                                            amount = _item.income_amount,
                                            date = _item.income_date,
                                            type = "Income",
                                            title = _item.intype,
                                            paymentmode = _item.paymentmode

                                        });
                                    }
                                    if (!string.IsNullOrEmpty(_item.Exid))
                                    {
                                        dailyReport.Add(new DailyReportModel()
                                        {
                                            icon = "decrease.png",
                                            amount = _item.expense_amount,
                                            date = _item.expence_date,
                                            type = "Expense",
                                            title = _item.exptype,
                                            paymentmode = _item.paymentmode

                                        });
                                    }
                                }
                                if (dailyReport.Count > 0)
                                {
                                    frm_reports.IsVisible = true;
                                }
                                else
                                {
                                    frm_reports.IsVisible = false;
                                }
                                lbl_expendse.Text = totalExp.ToString();
                                lbl_Income.Text = totalincome.ToString();
                                lbl_balance.Text = (totalincome - totalExp - (6000 * 31)).ToString();
                                BindableLayout.SetItemsSource(stk_reports, dailyReport);
                                UserDialogs.Instance.HideLoading();

                            }
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }

        public async Task GetAllIncome(string _month, string _year)
        {
            UserDialogs.Instance.ShowLoading("", MaskType.Black);
            APIResponse _apiresponse = new APIResponse();
            List<IncomeModel> _orders = new List<IncomeModel>();
            _apiresponse = await App._dataManager.GetAllIncome();
            try
            {
                if (_apiresponse == null)
                {
                    await GetAllExpences(_month, _year);
                    UserDialogs.Instance.HideLoading();
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    return;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            await GetAllExpences(_month, _year);
                            UserDialogs.Instance.HideLoading();
                            DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                            return;
                        }
                        else
                        {
                            _orders = _apiresponse.Data.ConvertAs<List<IncomeModel>>();

                            if (_orders != null)
                            {

                                foreach (var _item in _orders)
                                {


                                    if (!string.IsNullOrEmpty(_item.date))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.date);
                                        _item.date = _crDate.ToString("dd MMM yyyy");
                                        _item.month = _crDate.ToString("MMMM");
                                        _item.year = _crDate.ToString("yyyy");
                                        years.Add(_crDate.ToString("yyyy"));
                                    }
                                }
                                // string[] dist = years.Distinct().ToArray();
                                //combo_year.ItemsSource = dist;
                                //combo_year.Text = curYear;
                                // var _date = date_select.Date.ToString("dd MMM yyyy");
                                UpIncomes = new List<IncomeModel>();
                                UpIncomes = _orders.Where(x => x.month == _month && x.year == _year).ToList();


                                if (UpIncomes != null)
                                {
                                    UserDialogs.Instance.HideLoading();
                                    await GetAllExpences(_month, _year);
                                }
                            }
                            else
                            {
                                UserDialogs.Instance.HideLoading();
                                await GetAllExpences(_month, _year);

                            }
                        }
                    }
                    else
                    {
                        await GetAllExpences(_month, _year);
                        UserDialogs.Instance.HideLoading();
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                await GetAllExpences(_month, _year);
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ReportBindable_Tapped(object sender, EventArgs e)
        {

        }



        async private void combo_month_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            curMonth = e.Value.ToString();
            BindableLayout.SetItemsSource(stk_reports, null);
            await GetAllIncome(curMonth, curYear);
        }

        async private void combo_year_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            curYear = e.Value.ToString();
            BindableLayout.SetItemsSource(stk_reports, null);
            await GetAllIncome(curMonth, curYear);
        }
    }
}