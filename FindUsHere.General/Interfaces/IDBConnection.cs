using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.General.Interfaces
{
    public interface IDBConnection
    {
        #region User
        IUser RegisterUser(string username, string email, string password);
        IUser LoginUser(string username, string password);
        IUser UpdateUser(string username, string email, string password);
        int DeleteUser(int id);
        #endregion

        #region 
        IList<IBusinessInfo> GetAllBusinessInfosByRadius(double lat, double lon, int radius);

        IList<IBusinessInfo> GetAllBusinessInfosById(int id);

        IBusinessInfo InsertBusinessInfos(IBusinessInfo businessInfo);

        int DeleteBusiness(int Id);
        #endregion

        #region Category

        IList<ICategory> SelectAllCategories();
        #endregion

        #region Favorites 

        
        int CreateFavorite(int userId, int businessId);
        int DeleteFavorites(int userId, int businessId);
        #endregion

        #region Liked 

        
        int CreateLiked(int id, int businessId);

        int DeleteLiked(int userId, int businessId);
        #endregion

        #region Disliked

        int CreatDisliked(int id, int businessId);
        int DeleteDisliked(int userId, int businessId);

        #endregion

        

    }
}
