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
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage()
        {
            InitializeComponent(); 
            string[] vs = { "a", "b", "a", "b", "a", "b" };
            cv_services.ItemsSource = vs;
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void next_button_Clicked(object sender, EventArgs e)
        {

        }
    }
}