using FindUsHere.General;
using FindUsHere.General.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using FindUsHere.Model.Models;



namespace FindUsHere.Model.RestApi
{
    public class RestApiBusiness : RestApiBase
    {

        private string businessUrl = "/BusinessInfo";


        // Get Business
        public async Task<List<BusinessInfo>> GetBusinessInfosAsync()
        {

            List<BusinessInfo>? businessInfos = new();

            try
            {
                var business = await GetReponseContent(businessUrl + "/business");
                businessInfos = JsonConvert.DeserializeObject<List<BusinessInfo>>(business!);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return businessInfos;



        }

        // Post Business 
        public async Task<bool> PostBusinessAsync(IBusinessInfo businessInfo)
        {
            bool responce = false;
            try
            {
                responce = await PutDataToServerAsync(businessInfo, "/business");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }
    }
}
