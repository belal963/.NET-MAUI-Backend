using FindUsHere.General;
using FindUsHere.General.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using FindUsHere.ModelMaui.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;



namespace FindUsHere.ModelMaui.RestApi
{
    public class RestApiBusiness : RestApiBase
    {
        public bool UploadedFile = false;
        public long TotalBytes;
        public long UploadedBytes;
        private bool UploadToGoogle = true;
        public string UploadedMessage = "";
        private string containerName = "findushere-test";

        private string businessUrl = "/businessInfo";

        // Get Business
        public async Task<List<BusinessInfo>> GetBusinessInfosAsync(double lat, double lon, int radius)
        {

            List<BusinessInfo>? businessInfos = new();


            try
            {
                var business = await GetReponseContent(businessUrl + "/business?lat=" + $"{lat}" + "&lon=" + $"{lon}" + "&radius=" + $"{radius}");
                businessInfos = JsonConvert.DeserializeObject<List<BusinessInfo>>(business!);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return businessInfos;
        }

        public async Task<List<BusinessInfo>> GetBusinessInfosByIdAsync(int Id)
        {

            List<BusinessInfo>? businessInfos = new();

            try
            {
                var business = await GetReponseContent(businessUrl + "/business/id?id="+ $"{Id}");
                businessInfos = JsonConvert.DeserializeObject<List<BusinessInfo>>(business!);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return businessInfos;
        }

        public async Task<List<BusinessInfo>> GetFavorites(int userId)
        {

            List<BusinessInfo>? businessInfos = new();
            

            try
            {
                var business = await GetReponseContent("/user" + "/favorites?userId=" + $"{userId}");
                businessInfos = JsonConvert.DeserializeObject<List<BusinessInfo>>(business!);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return businessInfos;
        }

        public async Task<List<Category>> GetCategories()
        {
            List<Category> categorieslist = new();
            try
            {
                var categories = await GetReponseContent(businessUrl + "/categories");
                categorieslist = JsonConvert.DeserializeObject<List<Category>>(categories!);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return categorieslist;
        }


        // Post Business 
        public async Task<bool> PostBusinessAsync(BusinessInfo businessInfo1)
        {
            bool responce = false;
            try
            {
                responce = await PostDataToServerAsync(businessInfo1, businessUrl + "/business");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }


        public async Task<bool> DeleteBusinessAsync(int Id)
        {

            bool response = false;
            try
            {
                response = await DeleteToServerAsync(businessUrl + "/business/" + $"{Id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return response;
        }

        public async Task<bool> UploadFile(MultipartFormDataContent form)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }

            return await UploadFileRequest(businessUrl, form);

        }


        public async Task<bool> UploadFileToServer(byte[] fileData, string fileName)
        {
            var fileUpload = new FileUpload
            {
                Name = fileName,
                Type = "image/jpeg",
                File = fileData
            };

            bool response = false;
            try
            {
                response = await UploadFileToGoogleServer(businessUrl, fileUpload);
                return response;
            }
            catch
            { return false; }
        }

        public async Task<string> UploadLargeFile(string filePath, int userId)
        {
            UploadedBytes = 0;


            UploadedFile = true;

            var info = new FileInfo(filePath);
            TotalBytes = info.Length;
            long percent = 0;
            long chunkSize = 400000;
            long numChunks = TotalBytes / chunkSize;
            long remainder = TotalBytes % chunkSize;


            string justFileName = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);
            string newfileNameWithoutPath = $"{justFileName}-{DateTime.Now.Ticks}{extension}";

            bool firstChunk = true;

            using (var instream = File.OpenRead(filePath))
            {
                while (UploadedBytes < TotalBytes)
                {
                    var whatsLeft = TotalBytes - UploadedBytes;
                    if (whatsLeft > chunkSize)
                    {
                        chunkSize = remainder;
                    }
                    var bytes = new byte[chunkSize];
                    var buffer = new Memory<byte>(bytes);
                    var read = await instream.ReadAsync(buffer);

                    var chunk = new FileChunk
                    {
                        Data = bytes,
                        FileNameNoPath = newfileNameWithoutPath,
                        Offset = UploadedBytes,
                        FirstChunk = firstChunk
                    };


                    var photoLink = await UploadFileChunk(chunk, userId);

                    firstChunk = false;

                    UploadedBytes += read;
                    percent = UploadedBytes * 100 / TotalBytes;


                    return photoLink;
                }
               
            }
            UploadedFile = false;
            return null;
        }
    }
}