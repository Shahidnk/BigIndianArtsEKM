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
    public partial class ViewAdIncomePage : ContentPage
    {
        public ViewAdIncomePage()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            await GetAllExpences();
        }
        public async Task GetAllExpences()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetExpenceModel> _expenceModels = new List<GetExpenceModel>();
            _apiresponse = await App._dataManager.GetAllExpences();
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
                            DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                            cv_orders.ItemsSource = _expenceModels;
                            return;
                        }
                        else
                        {
                            _expenceModels = _apiresponse.Data.ConvertAs<List<GetExpenceModel>>();

                            var _expItem = _expenceModels.Where(x => x.Mode == "Income");
                            var tempItem = _expItem.OrderByDescending(x => x.createdDate);
                            if (tempItem != null)
                            {
                                foreach (var _item in tempItem)
                                {

                                    if (!string.IsNullOrEmpty(_item.expence_date))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.expence_date);
                                        _item.expence_date = _crDate.ToString("dddd, dd MMMM yyyy");
                                    }

                                }

                                cv_orders.ItemsSource = tempItem;
                            }
                            else
                            {
                                DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
                                cv_orders.ItemsSource = _expenceModels;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege(_apiresponse.Message);
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
            var _selectedBtn = _tapped.Parameter as GetExpenceModel;
            await Navigation.PushAsync(new EditIncomePage(_selectedBtn));
        }

        async private void CreateOrder_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAdIncomePage());
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}