using Acr.UserDialogs;
using BigIndianArts.Data;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BigIndianArts.Data.MediaStorageClient;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistOrderDetailsPage : ContentPage
    {
        private string _imageUrl;
        private string _finalimageUrl;
        private GetOrderModel _orderDataMain;
        private int _id;
        private string item;
        private string upValue;
        private string _orderId;

        public string TextFile { get; private set; }
        public string LocFile { get; private set; }
        public static FileResult MediaResults { get; internal set; }

        public ArtistOrderDetailsPage(string _id)
        {
            InitializeComponent();
            _orderId = _id;


        }
        async protected override void OnAppearing()
        {
            await GetAllProducts();
        }
        public async Task GetAllProducts()
        {
            APIResponse _apiresponse = new APIResponse();
            List<GetOrderModel> _orders = new List<GetOrderModel>();
            ArtistModel artist = new ArtistModel();
            artist.id = Convert.ToInt32(App._LoggedPersonNumber);
            _apiresponse = await App._dataManager.GetArtistOrders(artist);
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
                           // cv_orders.ItemsSource = _orders;
                            DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _orders = _apiresponse.Data.ConvertAs<List<GetOrderModel>>();

                            if (_orders != null)
                            {
                                foreach (var _item in _orders)
                                {

                                    if (!string.IsNullOrEmpty(_item.expected_date))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.expected_date);
                                        _item.expected_date = _crDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
                                    }
                                    if (!string.IsNullOrEmpty(_item.createdOn))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.createdOn);
                                        _item.createdOn = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                    }

                                    //if (_item.frame == "yes")
                                    //{
                                    //    _item.frame = "With Frame";
                                    //}
                                    //else
                                    //{
                                    //    _item.frame = "No Frame";
                                    //}
                                }
                                _orderDataMain = _orders.Where(x=>x.id==_orderId).FirstOrDefault();
                                _id = Convert.ToInt32(_orderDataMain.id);
                                // lbl_advanceAmount.Text = _orderDataMain.advance;
                                //lbl_BalanceAmount.Text = (Convert.ToInt32(_orderDataMain.price) - Convert.ToInt32(_orderDataMain.advance)).ToString();
                             //   lbl_orderAmount.Text = _orderDataMain.price;
                                lbl_BillNumber.Text = _orderDataMain.bill_number;
                                lbl_commision.Text = _orderDataMain.commission;
                                lbl_CustomerName.Text = _orderDataMain.customer_name;
                                lbl_artist.Text = _orderDataMain.assign_name;
                                lbl_createdDate.Text = _orderDataMain.createdOn;
                                lbl_CustomerNumber.Text = _orderDataMain.contact_number;
                                lbl_NumberFace.Text = _orderDataMain.people_no;
                                lbl_orderID.Text = _orderDataMain.id;
                                lbl_remarks.Text = _orderDataMain.remarks;
                                lbl_type.Text = _orderDataMain.type;
                                combo_status.Text = _orderDataMain.status;
                                lbl_expectedDate.Text = _orderDataMain.expected_date;
                                lbl_Size.Text = _orderDataMain.size;
                                lbl_Frame.Text = _orderDataMain.frame;
                                lbl_orientation.Text = _orderDataMain.orientation;
                                lbl_DeliveryMode.Text = _orderDataMain.delivery_mode.TrimStart(); 
                                lbl_courierService.Text = Path.GetFileName(_orderDataMain.uploaded_image);
                                if (_orderDataMain.status == "Completed")
                                {
                                    stk_footer.IsVisible = false;
                                    bx_completed.IsVisible = true;
                                    lbl_Completed.IsVisible = true;
                                    lbl_completedTitle.IsVisible = true;
                                    bx_ImageUpload.IsVisible = false;
                                    lbl_ImageUpload.IsVisible = false;
                                    stk_ImageUpload.IsVisible = false;
                                    combo_status.IsEnabled = false;
                                }
                                else
                                {
                                    stk_footer.IsVisible = true;
                                    bx_completed.IsVisible = false;
                                    lbl_Completed.IsVisible = false;
                                    lbl_completedTitle.IsVisible = false;
                                }
                                if (_orderDataMain.isVideo == "True")
                                {
                                    lbl_isVideo.Text = "Yes";
                                }
                                else
                                {
                                    lbl_isVideo.Text = "No";
                                }
                                if (_orderDataMain.online_order == "True")
                                {
                                    lbl_isOnline.Text = "Yes";
                                }
                                else
                                {
                                    lbl_isOnline.Text = "No";
                                }
                                lbl_Completed.Text = Path.GetFileName(_orderDataMain.final_image);

                                _imageUrl = _orderDataMain.uploaded_image;
                                _finalimageUrl = _orderDataMain.final_image;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        private void timelineListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Image_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(_imageUrl, BrowserLaunchMode.SystemPreferred);
        }

        private void Combo_status_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (e.Value.ToString() == "Completed")
            {
                upValue = "Waiting For Approval";
                stk_footer.IsVisible = true;
                bx_ImageUpload.IsVisible = true;
                lbl_ImageUpload.IsVisible = true;
                stk_ImageUpload.IsVisible = true;
            }
            else if (e.Value.ToString() == "Processing")
            {
                upValue = "Processing";
                stk_footer.IsVisible = true;
                bx_ImageUpload.IsVisible = false;
                lbl_ImageUpload.IsVisible = false;
                stk_ImageUpload.IsVisible = false;
            }
            else
            {
                upValue = e.Value.ToString();
                stk_footer.IsVisible = false;
                bx_ImageUpload.IsVisible = false;
                lbl_ImageUpload.IsVisible = false;
                stk_ImageUpload.IsVisible = false;
            }

        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {

        }

        async private void submit_clicked(object sender, EventArgs e)
        {
            await UpdateOrder(_id, upValue);
        }

        public async Task UpdateOrder(int _id, string _status)
        {
            APIResponse _apiResponce = new APIResponse();
            UpdateOrderModel _orderData = new UpdateOrderModel();

            DateTime _now = DateTime.Now;
            _orderData.id = _id;
            _orderData.status = _status;
            _orderData.image = item;
            _orderData.user_id = _orderDataMain.assign_id;
            _orderData.user_name = _orderDataMain.assign_name;
            _orderData.title = _orderDataMain.type;
            _orderData.createdOn = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);



            _apiResponce = await App._dataManager.UpdateOrderStatus(_orderData);

            if (_apiResponce == null)
            {
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Data Null");

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

                DependencyService.Get<IAlertPlayer>().AlertMessege("Order Updated Successfully.");

                await Navigation.PopAsync();
            }
        }

        async private void ImageButtonClicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var result = await FilePicker.PickAsync();
            //    if (result != null)
            //    {
            //        TextFile = $"File Name: {result.FileName}";
            //        LocFile = result.FullPath;

            //    }
            //    await AddSignToSTorage(LocFile);


            //}
            //catch (Exception ex)
            //{
            //    // The user canceled or something went wrong
            //}

            MediaPopup mediaPage = new MediaPopup(2);
            mediaPage.Disappearing += MediaPage_Disappearing;
            await PopupNavigation.Instance.PushAsync(mediaPage);
        }
        async private void MediaPage_Disappearing(object sender, EventArgs e)
        {
            if (MediaResults != null)
            {
                TextFile = $"File Name: {MediaResults.FileName}";
                LocFile = MediaResults.FullPath;

                var stream = MediaResults.OpenReadAsync().Result;
                byte[] _imgByte = await ResizeImage(stream);
                await AddSignToSTorage(_imgByte, MediaResults.FileName);
            }
          
        }
        private async Task AddSignToSTorage(byte[] _fileBytes,string _fileName)
        {
            LoadingPage loadingPage = new LoadingPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            UserDialogs.Instance.ShowLoading("", MaskType.Black);
            APIResponse _apiResponce = new APIResponse();
            try
            {

                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.ConstrainedInternet || current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {


                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "No network connection", "OK");

                    return;
                }

                StoredMediaAssets _addstoragerslt = new StoredMediaAssets();
                List<StoredMediaAssets> _mediaAssetRslt = new List<StoredMediaAssets>();
                MediaStorageClient _mediastorageclient = new MediaStorageClient();

                //List<string> _files = new List<string>();
                //_files.Add(fileData);

                //  _apiResponce = await _mediastorageclient.AddToStorage(_files);
                _apiResponce = await _mediastorageclient.AddToStorage( _fileBytes, _fileName);

                if (_apiResponce == null)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                    Application.Current.MainPage = new FlyoutPage();
                    return;
                }

                if (_apiResponce.IsSuccess != true)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();

                    await DisplayAlert("Alert", "API Responce Error", "OK");

                    return;
                }
                if (_apiResponce.Data == null)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();

                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");


                    return;
                }

                _addstoragerslt = _apiResponce.Data.ConvertAs<StoredMediaAssets>();

                item = _addstoragerslt.Url;
                if (!string.IsNullOrWhiteSpace(item))
                {

                    ent_ImageUpload.Text = Path.GetFileName(item);
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();

                }
                else
                {
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {

                if (PopupNavigation.PopupStack.Count > 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }

                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                Application.Current.MainPage = new FlyoutPage();

            }
        }

        async private void CompletedImage_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(_finalimageUrl, BrowserLaunchMode.SystemPreferred);
        }
        protected async Task<byte[]> ResizeImage(Stream stream)
        {
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            //if (Device.RuntimePlatform == Device.Android)
            //{
            byte[] resizedImage = await DependencyService.Get<IImageResizer>().ResizeImage(imageData);
            // image.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
            return resizedImage;
            //}
            //else
            //{
            //    return imageData;
            //}
        }
    }
}