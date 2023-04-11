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
    public partial class AllCategoryPage : ContentPage
    {
        public AllCategoryPage()
        {
            InitializeComponent();
            string[] vs = { "a", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b", "b", "a", "b", "a", "b" };
            cv_services.ItemsSource = vs;
        }

        private void ServiceListItem_Tapped(object sender, EventArgs e)
        {

        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
         }
    }
}