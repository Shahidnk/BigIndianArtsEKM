using BigIndianArts.ViewModels;
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
    public partial class OrderHistoryPage : ContentPage
    {
       
        public OrderHistoryPage()
        {
            InitializeComponent();
           
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void next_button_Clicked(object sender, EventArgs e)
        {

        }
    }
    
}