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
    public partial class EditExpencePage : ContentPage
    {
        private string _id;

        public EditExpencePage(GetExpenceModel _getExpence)
        {
            InitializeComponent();
            _id = _getExpence.id;
            ent_amount.Text = _getExpence.amount;
            ent_expence.Text = _getExpence.type;
            combo_payMethod.Text = _getExpence.paymentmode;
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Update_clicked(object sender, EventArgs e)
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
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select  payment mode");
                combo_payMethod.Focus();
                return;
            }
            ExpenceModel _expenceModel = new ExpenceModel();
            APIResponse _apiResponce = new APIResponse();

            _expenceModel.id = _id;
            _expenceModel.amount = ent_amount.Text;
            _expenceModel.type = ent_expence.Text;
            _expenceModel.paymentmode = combo_payMethod.Text;
            _expenceModel.createdOn = DateTime.Now;
            _apiResponce = await App._dataManager.UpdateExpences(_expenceModel);

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


                DependencyService.Get<IAlertPlayer>().AlertMessege("Expence Updated Successfully");
                await Navigation.PopAsync();
            }
            else
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Expence Updated Failed");
            }

        }

        async private void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}