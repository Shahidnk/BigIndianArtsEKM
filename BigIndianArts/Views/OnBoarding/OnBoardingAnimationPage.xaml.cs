using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views.OnBoarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBoardingAnimationPage : ContentPage
    {
        public OnBoardingAnimationPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await App.NetworkCheck(this, true);
            if (App._isnetwrk == false)
            {
                return;
            }
            Application.Current.Properties["ISOnscreenViewed"] = Boolean.TrueString;
            await Application.Current.SavePropertiesAsync();
            App._IsOnscreenViewed = true;


        }
    }
}