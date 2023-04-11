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
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
            img_Wishlistred.IsVisible = false;
            string[] vs = { "a", "b", "a", "b" };
            BindableLayout.SetItemsSource(stk_cartItems, vs);
        }
       
        protected override void OnAppearing()
        {

            List<RotatorModel> list = new List<RotatorModel>();
           
            list.Add(new RotatorModel() { Image = "demo1.jpg", Id =1, RequestId = 2 });
            list.Add(new RotatorModel() { Image = "demo2.jpg", Id = 3, RequestId = 2 });
            list.Add(new RotatorModel() { Image = "demo3.jpg", Id=2,RequestId=2 });

          


           
            sfRotator_Home.ItemsSource = list;


           

           
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void BannerImg_clicked(object sender, EventArgs e)
        {

        }

       async private void AddToCartButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage(1));
        }

        private void WishList_Clicked(object sender, EventArgs e)
        {
            if (img_WishlistWhite.IsVisible == true)
            {
                img_Wishlistred.IsVisible = true;
                img_WishlistWhite.IsVisible = false;
            }
            else
            {
                img_WishlistWhite.IsVisible = true; 
                img_Wishlistred.IsVisible = false;
            }
        }
    }
}