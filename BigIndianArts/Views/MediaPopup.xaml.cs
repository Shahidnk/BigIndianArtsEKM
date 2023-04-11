using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private int _page;

        public MediaPopup(int _page)
        {
            InitializeComponent();

            this._page = _page;
        }

        async private void Camera_clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (_page == 1)
                {
                    OrderRequestPage.MediaResults = result;
                }
                else if (_page == 2)
                {
                    ArtistOrderDetailsPage.MediaResults = result;
                }


                if (PopupNavigation.PopupStack.Count > 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }

            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

        }

        async private void Gallery_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (_page == 1)
                {
                    OrderRequestPage.MediaResults = result;
                }
                else if (_page == 2)
                {
                    ArtistOrderDetailsPage.MediaResults = result;
                }

                if (PopupNavigation.PopupStack.Count > 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }

            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }
}