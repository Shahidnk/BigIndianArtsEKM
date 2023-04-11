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
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            web_PrivacyPolicy.Source = "https://en.wikipedia.org/wiki/Privacy_policy#:~:text=A%20privacy%20policy%20is%20a,a%20customer%20or%20client's%20data.";
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}