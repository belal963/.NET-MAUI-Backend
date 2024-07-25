using FindUsHere.General.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FindUsHere.General;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using FindUsHere.RestApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace FindUsHere.RestApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        #region ctor
        public UserController(IDBConnection dBConnection) : base(dBConnection)
        {
        }
        #endregion

        #region user
        
        [HttpPost("user/register")]
        public async Task<IActionResult> RegisterUser(string username, string email, string password)
        {
            return await TryCatch(() =>
            {
                var registerUser = _connection.RegisterUser(username, email,password);
                if (registerUser != null)
                {
                    return JsonSerializer.Serialize<IUser>(registerUser);
                }
                else
                {
                    throw new NotFoundException("User already Exist");
                }
            });
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> LoginUser(string username, string password)
        {
            return await TryCatch(() => {
                
                var loginUser =_connection.LoginUser(username, password);
                if (loginUser != null)
                {
                    // Todo: find out why only the lists are serialized, if it is not serialized here before (mkrietsch)
                    return JsonSerializer.Serialize<IUser>(loginUser);
                }
                else 
                {
                    throw new NotFoundException("User does not Exist");
                }
            });
        }

        [HttpPut("user/update")]
        public async Task<IActionResult> UpdateUser(string username, string email, string password)
        {
            return await TryCatch(() => {
                var updateUser = _connection.UpdateUser(username, email, password);
                if (updateUser != null)
                {
                    return JsonSerializer.Serialize<IUser>(updateUser);
                }
                else
                {
                    throw new NotFoundException("User does not Exist");
                }
            });

        }

        [HttpDelete("user/delete")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            return await TryCatch(() =>
            {
                var deletedUser = _connection.DeleteUser(userId);
                if (deletedUser == 0)
                {
                    throw new NotFoundException("there is not user to delete");
                }

            });
            
                
        }
        #endregion

        #region Favorites 

        [HttpPut("favorites")]
        public async Task<IActionResult> CreateFavorites(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var favorites = _connection.CreateFavorite(userId, businessId);
                if (favorites == 0)
                {
                    throw new NotFoundException("it is already in your favorites");
                }
            });
        }

        [HttpDelete("favorites")]
        public async Task<IActionResult> DeleteFavorites(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var favorites = _connection.DeleteFavorites(userId, businessId);
                if (favorites == 0)
                {
                    throw new NotFoundException("something went wrong");
                }
            });
        }

        #endregion

        #region Liked

        [HttpPut("liked")]
        public async Task<IActionResult> CreateLiked(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var like = _connection.CreateLiked(userId, businessId);
                if (like == 0)
                {
                    throw new NotFoundException("something went wrong");
                }
            });
        }

        [HttpDelete("liked")]
        public async Task<IActionResult> DeleteLiked(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var like = _connection.DeleteLiked(userId, businessId);
                if (like == 0)
                {
                    throw new NotFoundException("something went wrong");
                }
            });
        }

        #endregion

        #region Disliked


        [HttpPut("disliked")]
        public async Task<IActionResult> CreatDisliked(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var dislike = _connection.CreatDisliked(userId, businessId);
                if (dislike == 0)
                {
                    throw new NotFoundException("something went wrong");
                }
            });
        }

        [HttpDelete("disliked")]
        public async Task<IActionResult> DeleteDisliked(int userId, int businessId)
        {
            return await TryCatch(() =>
            {
                var dislike = _connection.DeleteDisliked(userId, businessId);
                if (dislike == 0)
                {
                    throw new NotFoundException("something went wrong");
                }
            });
        }

        #endregion
    }
}
