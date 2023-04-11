using Acr.UserDialogs;
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
    public partial class CreateExpencePage : ContentPage
    {
        public CreateExpencePage()
        {
            InitializeComponent();
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void submit_clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ent_expence.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter expense");
                ent_expence.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_amount.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter amount");
                ent_amount.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_payMethod.Text))
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter remarks");
                combo_payMethod.Focus();
                return;
            }
            ExpenceModel _expenceModel = new ExpenceModel();
            APIResponse _apiResponce = new APIResponse();
            DateTime _now = DateTime.Now;
            _expenceModel.amount = ent_amount.Text;
            _expenceModel.type = ent_expence.Text;
            _expenceModel.paymentmode = combo_payMethod.Text;
            _expenceModel.createdOn = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);



            //_expenceModel.remarks = ent_remarks.Text;


            _apiResponce = await App._dataManager.CreateExpences(_expenceModel);

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


                DependencyService.Get<IAlertPlayer>().AlertMessege("Expence Created Successfully");
                await Navigation.PopAsync();
            }

        }

        async private void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}