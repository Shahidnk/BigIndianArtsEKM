using BigIndianArts.Interface;
using BigIndianArts.Models;
using Flurl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigIndianArts.Data
{
    public class DataManager : IDisposable
    {
        public IRestService Service;

        readonly string BaseURL = App.BaseURL;

        public DataManager()
        {
            Service = new RestService();
        }


        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        #region GET
        public async Task<APIResponse> GetAllProducts()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("getproducts.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }

        public async Task<APIResponse> GetAllOrders()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetOrderRequest.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }
        public async Task<APIResponse> GetAllIncome()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("getallincome.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }

        public async Task<APIResponse> GetAllExpences()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetExpences.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }

        public async Task<APIResponse> GetAllArtists()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetArtists.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }

        public async Task<APIResponse> GetArtistsDetails()
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetArtistDetails.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.GET<APIResponse>(_url);
        }
        #endregion

        #region POST
        public async Task<APIResponse> LoginTask(LoginModel _loginDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("login.php");
            //_url.QueryParams["Username"] = _loginDetails.Username;
            //_url.QueryParams["Password"] = _loginDetails.Password;
            return await Service.POST<APIResponse>(_url, _loginDetails);
        }
        public async Task<APIResponse> OrderTask(OrderModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("createorderrequest.php");
            //_url.QueryParams["Username"] = _loginDetails.Username;
            //_url.QueryParams["Password"] = _loginDetails.Password;
            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> EditOrderTask(OrderModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("Editorderrequest.php");
            //_url.QueryParams["Username"] = _loginDetails.Username;
            //_url.QueryParams["Password"] = _loginDetails.Password;
            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> UpdateOrderTask(NotificationModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("updateOrderRequest.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> DeviceTokenSaveAsync(DeviceTokensModel  _deviceTokens)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("DeviceToken.php");

            return await Service.POST<APIResponse>(_url, _deviceTokens);
        }
        public async Task<APIResponse> UpdateOrderStatus(UpdateOrderModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("UpdateOrderStaus.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }

        public async Task<APIResponse> UpdateOrderSettlements(UpdateOrderModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("updateSettlement.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> DeleteOrder(DeleteOrder _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("deleteorder.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }

        public async Task<APIResponse> UpdateOrderAction(UpdateAction _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("UpdateActionOrder.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> CreateArtist(ArtistModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("CreateArtistLogin.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }
        public async Task<APIResponse> EditArtist(ArtistModel _orderDetails)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("EditArtistPage.php");

            return await Service.POST<APIResponse>(_url, _orderDetails);
        }

        public async Task<APIResponse> CreateExpences(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("createExpences.php");

            return await Service.POST<APIResponse>(_url, _expenceModel);
        } 
        public async Task<APIResponse> CreateIncome(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("createIncome.php");

            return await Service.POST<APIResponse>(_url, _expenceModel);
        }
        public async Task<APIResponse> Settlecommission(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("settlecommission.php");

            return await Service.POST<APIResponse>(_url, _expenceModel);
        }
        public async Task<APIResponse> SavecommissionExp(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("CommissionAddExpense.php");

            return await Service.POST<APIResponse>(_url, _expenceModel);
        }

        public async Task<APIResponse> UpdateExpences(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("EditExpenses.php");

            return await Service.POST<APIResponse>(_url, _expenceModel);
        }
        public async Task<APIResponse> UpdateIncome(ExpenceModel _expenceModel)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("EditExpenses.php");// Additional Income is on expence tabel;

            return await Service.POST<APIResponse>(_url, _expenceModel);
        }
        public async Task<APIResponse> GetAllMenu(MenuPost _data)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("menu_items.php");
            //_url.QueryParams["Username"] = _loginDetails.Username;
            //_url.QueryParams["Password"] = _loginDetails.Password;
            return await Service.POST<APIResponse>(_url, _data);
        }
        public async Task<APIResponse> GetArtistOrders(ArtistModel artist)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetArtistRequests.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.POST<APIResponse>(_url, artist);
        }

        public async Task<APIResponse> GetNotification(NotificationModel artist)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetNotification.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.POST<APIResponse>(_url, artist);
        }
        public async Task<APIResponse> GetCommission(NotificationModel artist)
        {
            Url _url = new Url(BaseURL).AppendPathSegment("GetCommision.php");
            //_url.SetQueryParam("ID", App._LoggedUserId);
            return await Service.POST<APIResponse>(_url, artist);
        }
        #endregion
    }
}
