using FindUsHere.General.Interfaces;
using FindUsHere.ModelMaui.Models;
using Newtonsoft.Json;
using System.Text;

namespace FindUsHere.ModelMaui.RestApi
{
    public class RestApiUser : RestApiBase
    {
        private string userUrl = "/user";
        private string UserName = "";

        // Get User
        public async Task<List<User>> GetUserAsync(string UserName)
        {

            List<User>? userlist = new();

            try
            {
                var user = await GetReponseContent(userUrl + "/user?UserName=" + $"{UserName}");
                userlist = JsonConvert.DeserializeObject<List<User>>(user!);
                //
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return userlist;



        }


        public async Task<User> PostUserAsync(string UserName,string Password, string UserEmail)
        {

            try
            {
                var responce = await UserPostToServerAsync(userUrl + "/user/register?UserName=" + $"{UserName}" + "&email=" + $"{UserEmail}"+ "&password=" + $"{Password}");
                return responce;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
                return null;
            }
           
        }

        public async Task<User> PostUserLoginAsync(string UserName, string Password)
        {

            try
            {
               var responce = await UserPostToServerAsync(userUrl + "/user/login?UserName=" + $"{UserName}" + "&password=" + $"{Password}");
                return responce;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
                return null;
            }
            
        }

        public async Task<bool> PutUserAsync(string UserName, string Password, string UserEmail)
        {

            bool responce = false;
            try
            {
                responce = await UserPutToServerAsync(userUrl + "/user/update?UserName=" + $"{UserName}" + "&email=" + $"{UserEmail}" + "&password=" + $"{Password}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }

        public async Task<bool> DeleteUserAsync(int Id)
        {

            bool responce = false;
            try
            {
                responce = await DeleteToServerAsync(userUrl + "/user/delete/" + $"{Id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }


        public async Task<bool> SendToFavourited(int businessId, int userId)
        {
            bool response = false;
            try
            {
                response = await PutToServerAsync(userUrl + "/favorites/?userId=" + $"{userId}" + "&businessId=" + $"{businessId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP Request Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return response;

        }

        public async Task<bool> DeleteFav(int userId, int businessId)
        {

            bool responce = false;
            try
            {
                responce = await DeleteToServerAsync(userUrl + "/favorites/?userId=" + $"{userId}" + "&businessId=" + $"{businessId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }


        public async Task<bool> SendToLiked(int businessId, int userId)
        {
            bool response = false;
            try
            {
                response = await PutToServerAsync(userUrl + "/liked/?userId=" + $"{userId}" + "&businessId=" + $"{businessId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP Request Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return response;

        }


        public async Task<bool> SendToDislinked(int businessId, int userId)
        {
            bool response = false;
            try
            {
                response = await PutToServerAsync(userUrl + "/disliked/?userId=" + $"{userId}" + "&businessId=" + $"{businessId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP Request Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return response;

        }
    }
}
