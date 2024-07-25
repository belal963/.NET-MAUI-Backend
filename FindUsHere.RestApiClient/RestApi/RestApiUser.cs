using FindUsHere.General.Interfaces;
using FindUsHere.Model.Models;
using Newtonsoft.Json;
using System.Text;

namespace FindUsHere.Model.RestApi
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

        // Put User 

        public async Task<bool> PostUserAsync(IUser user1)
        {
            IUser user = new User();
            user.UserName = user1.UserName;
            user.UserEmail = user1.UserEmail;
            user.Password = user1.Password;
            user.Id = user1.Id;
            user.Favorites = new List<IFavorites>();
            user.Likeds = new List<ILiked>();
            user.Dislikeds = new List<IDisliked>();


            bool responce = false;
            try
            {
                responce = await PutDataToServerAsync(user, userUrl + "/user");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Did not Work: " + ex.Message);
            }
            return responce;
        }

    }
}
