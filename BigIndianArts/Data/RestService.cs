using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using Flurl;
using Newtonsoft.Json;

namespace BigIndianArts.Data
{
    public class RestService : IRestService
    {
        HttpClient Client;

        public RestService()
        {
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromMinutes(5);
            Client.MaxResponseContentBufferSize = 300600000;
           // Client.DefaultRequestHeaders.Add("X_UserID", App._LoggedUserId.ToString());
            //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App._loginToken);
        }

        public async Task<T> GET<T>(Url url)
        {
            try
            {
                HttpResponseMessage _response = await Client.GetAsync(url);
                string _responseString = await _response.Content.ReadAsStringAsync();
                var _result = JsonConvert.DeserializeObject<T>(_responseString);

                return _result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catch caught" + ex.Message);
            }

            return default(T);
        }

       

        public async Task<T> POST<T>(Url url, object data)
        {
            try
            {
                string contentstring = JsonConvert.SerializeObject(data);
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                StringContent _content = data != null ?
                new StringContent(contentstring, Encoding.UTF8, "application/json") : null;
                // HttpResponseMessage response = Client.PostAsync(url, _content).GetAwaiter().GetResult();
                HttpResponseMessage response = await Client.PostAsync(url, _content);
                string _result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(_result);

            }
            catch (Exception ex)
            {

            }

            return default(T);
        }
        public async Task<T> POSTFILE<T>(Url url, string ModuleName, FileToUpload file)
        {
            try
            {
                using (MultipartFormDataContent _content = new MultipartFormDataContent())
                {
                    _content.Add(new StringContent(ModuleName), "ModuleName");

                    if (file.ByteArray != null)
                    {
                        _content.Add(new ByteArrayContent(file.ByteArray, 0, file.ByteArray.Length), "files", file.FileName);
                    }

                    HttpResponseMessage response = await Client.PostAsync(url, _content);
                    string _result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(_result);
                }
            }
            catch (Exception ex)
            {

            }


         

            return default(T);
        }

        public async Task<T> DELETE<T>(Url url)
        {
            try
            {
                //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App._loginToken);

                HttpResponseMessage _response = await Client.DeleteAsync(url);

                if (_response.IsSuccessStatusCode)
                {
                    string _result = await _response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(_result);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {

            }

            return default(T);
        }
    }
}
