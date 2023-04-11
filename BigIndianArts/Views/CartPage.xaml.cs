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
    public partial class CartPage : ContentPage
    {
        public CartPage(int _page=0)
        {
            InitializeComponent();
            
            string[] vs = { "a", "b" };
            BindableLayout.SetItemsSource(stk_cartItems, vs);
        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

       async private void next_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderDetailPage());

        }

        private void kart_clicked(object sender, EventArgs e)
        {

        }

       async private void user_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());

        }

        private void homemenutapped(object sender, EventArgs e)
        {

        }

        private void ImageNotifyButton_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureProfileRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}