using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FindUsHere.General;
using FindUsHere.General.Interfaces;
using FindUsHere.ModelMaui.Models;
using Microsoft.Maui.Controls.Compatibility;
using Newtonsoft.Json;

namespace FindUsHere.ModelMaui.RestApi
{
    public class RestApiBase
    {
        private const string apiBaseUrl = "http://192.168.41.92:5123/api/v1";
        protected RestApiBase()
        {
          
        }

        // HTTP Get
        public async Task<string?> GetReponseContent(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage? response = await client.GetAsync(apiBaseUrl + url);
                if (response.IsSuccessStatusCode)
                {
                    var test = await response.Content.ReadAsStringAsync();
                    return test;
                }
                throw new HttpRequestException();
            }
        }

        // HTTP Post 
        public async Task<bool> PostDataToServerAsync(object data, string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiBaseUrl + url, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
        }

        public async Task<bool> PutDataToServerAsync(object data, string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiBaseUrl + url, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> UserPostToServerAsync(string url)
        {
            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(apiBaseUrl + url, null);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(jsonContent);
                    return user;
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<bool> UserPutToServerAsync(string url)
        {
            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PutAsync(apiBaseUrl + url, null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteToServerAsync(string url)
        {
            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(apiBaseUrl + url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> PutToServerAsync(string url)
        {

            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PutAsync(apiBaseUrl + url, null);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UploadFileRequest(string url, MultipartFormDataContent form)
        {
            try
            {
                using HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(5)
                };
                var response = await client.PostAsync(apiBaseUrl + url + "/uploadFile", form);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<string> UploadFileChunk(FileChunk fileChunk, int userId)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync(apiBaseUrl + "/businessInfo" + "/googlePostFile/"+ $"{userId}", fileChunk);
                string responseBody = await result.Content.ReadAsStringAsync();
                return responseBody;
            }catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> UploadFileToGoogleServer(string url, FileUpload fileUpload)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync(apiBaseUrl + url + "/googlePostFile", fileUpload);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> CopyFileToContainer(string ContainerName, string fileNamePath)
        {

            try
            {
                using HttpClient client = new HttpClient();
                var result = await client.GetAsync(apiBaseUrl);
                return await result.Content.ReadAsStringAsync();
            }catch (Exception ex)
            {
                var msg = ex.Message;
                return "";
            }


        }



    }
}
