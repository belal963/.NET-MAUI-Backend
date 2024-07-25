using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.Model.RestApi
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
            catch (HttpRequestException)
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
    }
}
