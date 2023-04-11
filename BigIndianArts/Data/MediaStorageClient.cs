using BigIndianArts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BigIndianArts.Data
{
    public class MediaStorageClient
    {
        public async Task<APIResponse> AddToStorage(List<string> files, int maxFileSizeInKb = 0, List<string> validExtensions = null)
        {
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if (maxFileSizeInKb > 0 && fileInfo.Length > 0)
                    {
                        if ((fileInfo.Length / 1024) < maxFileSizeInKb)
                        {

                        }
                    }

                    string currentFileExtension = Path.GetExtension(file);

                    if (validExtensions != null && !validExtensions.Contains(currentFileExtension))
                    {
                    }

                    formDataContent.Add(new ByteArrayContent(File.ReadAllBytes(file)), "image", Path.GetFileName(file));
                }

                HttpResponseMessage response = await client.PostAsync(
                    $"https://bigindianarts.ml/api_new/fileupload.php",
                    formDataContent);

                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(responseContent);
            }
        }


        public async Task<APIResponse> AddToStorage(Stream fileData, string _userinfo, string _coverID, int maxFileSizeInKb = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();

                if (maxFileSizeInKb > 0 && fileData.Length > 0)
                {
                    if ((fileData.Length / 1024) < maxFileSizeInKb)
                    {

                    }
                }

                formDataContent.Add(new StreamContent(fileData), "image", "Demo.jpg");


                HttpResponseMessage response = await client.PostAsync(
                $"http://66b3-35-245-127-51.ngrok.io/predict?user_info_={_userinfo}&cover_pg_id_={_coverID}",
                formDataContent);

                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(responseContent);
            }
        }


        public async Task<APIResponse> AddToStorage( byte[] fileData, string fileName, int maxFileSizeInKb = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formDataContent = new MultipartFormDataContent();

                if (maxFileSizeInKb > 0 && fileData.Length > 0)
                {
                    if ((fileData.Length / 1024) < maxFileSizeInKb)
                    {

                    }
                }

                formDataContent.Add(new ByteArrayContent(fileData), "image", fileName);

                HttpResponseMessage response = await client.PostAsync(
                   $"https://bigindianarts.ml/api_new/fileupload.php",
                   formDataContent);

                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(responseContent);
            }
        }

        public class StoredMediaAssets
        {
            public string Url { get; set; }
            public string FileName { get; set; }
            public string Message { get; set; }
        }
    }
}
