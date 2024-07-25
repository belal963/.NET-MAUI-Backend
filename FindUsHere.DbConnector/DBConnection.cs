using FindUsHere.DbConnector.Models;
using FindUsHere.General;
using FindUsHere.General.Interfaces;
using Geolocation;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using static LinqToDB.Common.Configuration;

namespace FindUsHere.DbConnector
{
    #region DbFactory
    public static class DbFactory
    {
        public static IDBConnection Create()
        {
            return new DBConnection();
        }
    }
    #endregion

    internal class DBConnection : IDBConnection
    {
        #region Properties 
        private ConnectorBase _connector;
        #endregion

        #region Ctor
        public DBConnection()
        {
            _connector = ConnectorBase.GetInstance();
        }
        #endregion

        #region User

        IUser IDBConnection.RegisterUser(string username, string email, string password)
        {
            try
            {
                var insertedUser = _connector.User.InsertWithOutput(new DbUser(username, email, password));

                return insertedUser;
            }
            catch (AlreadyExistsException)
            {
                throw;
            }
        }

        IUser IDBConnection.LoginUser(string username, string password)
        {
            try
            {   
                    var theUser = from infos in _connector.User
                                .LoadWith(x => x.Favorites)
                                .LoadWith(x => x.Likeds)
                                .LoadWith(x => x.Dislikeds)
                                  where infos.UserName == username &
                                  infos.Password == password
                                  select infos;
                    var userwithlists = theUser.ToList().FirstOrDefault();
                    return userwithlists;
            }
            catch (NotFoundException)
            {
                throw;
            }
        }

        IUser IDBConnection.UpdateUser(string username, string email, string password)
        {
            try
            {
                var db = _connector;
                var result = from infos in db.User where infos.UserName == username select infos;
                DbUser user = new DbUser(username, email, password);

                if (result.Any())
                {
                    DbUser userToUpdate = (DbUser)result.Single();
                    userToUpdate.UserEmail = email;
                    userToUpdate.Password = password;
                    db.Update(userToUpdate);
                    var theUser = from infos in result
                                .LoadWith(x => x.Favorites)
                                .LoadWith(x => x.Likeds)
                                .LoadWith(x => x.Dislikeds)
                                  where infos.UserName == username
                                  select infos;
                    var userwithlists = theUser.ToList().FirstOrDefault();
                    return userwithlists;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        int IDBConnection.DeleteUser(int id)
        {
            try
            {
                return _connector.Delete(new DbUser(id));
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Business
        IList<IBusinessInfo> IDBConnection.GetAllBusinessInfosByRadius(double lat, double lon, int radius)
        {
            try
            {
                CoordinateBoundaries boundaries = new(lat, lon, radius);
                double minLatitude = boundaries.MinLatitude;
                double maxLatitude = boundaries.MaxLatitude;
                double minLongitude = boundaries.MinLongitude;
                double maxLongitude = boundaries.MaxLongitude;

                var result = from infos
                             in _connector.BusinessInfo
                             .LoadWith(x => x.Category)
                             .LoadWith(x => x.User)
                             .LoadWith(x => x.PhotoLinks)
                             .LoadWith(x => x.Hours)
                             .ThenLoad(x =>x.Days)
                             where infos.GpsLatitude >= minLatitude && infos.GpsLatitude <= maxLatitude &&
                               infos.GpsLongitude >= minLongitude && infos.GpsLongitude <= maxLongitude
                             select infos;
                var resultGps = result.ToList<IBusinessInfo>();

                    return resultGps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        IList<IBusinessInfo> IDBConnection.GetAllBusinessInfosById(int id)
        {
            try
            {
                var result = from info
                             in _connector.BusinessInfo
                             .LoadWith(x => x.Category)
                             .LoadWith(x => x.User)
                             .LoadWith(x => x.PhotoLinks)
                             .LoadWith(x => x.Hours)
                             .ThenLoad(x => x.Days)
                             where info.User_FK == id
                             select info;

                return result.ToList<IBusinessInfo>();
            }
            catch (Exception)
            {
                throw;
            }


        }

        IBusinessInfo IDBConnection.InsertBusinessInfos(IBusinessInfo businessInfo)
        {
            try
            {
                var db = _connector;
                var userExists = db.GetTable<DbUser>().Any(u => u.Id == 1);
                if (!userExists)
                {
                    throw new Exception("The specified user does not exist.");
                }
                var dbBusinessInfo = new DbBusinessInfo
                {
                    Category_FK = 1,
                    User_FK = 1,
                    Title = businessInfo.Title,
                    Description = businessInfo.Description,
                    PhoneNumber = businessInfo.PhoneNumber,
                    Email = businessInfo.Email,
                    Website = businessInfo.Website,
                    PostCode = businessInfo.PostCode,
                    City = businessInfo.City,
                    Street = businessInfo.Street,
                    HouseNumber = businessInfo.HouseNumber,
                    Addition = businessInfo.Addition,
                    GpsLongitude = businessInfo.GpsLongitude,
                    GpsLatitude = businessInfo.GpsLatitude
                };
                var insertedBusinessInfo = db.GetTable<DbBusinessInfo>().InsertWithOutput(dbBusinessInfo);

                if (businessInfo.Hours != null)
                {
                    foreach (var hours in businessInfo.Hours)
                    {
                        var dbHours = new DbHours
                        {
                            BusinessInfo_FK = insertedBusinessInfo.Id,
                            Days_FK = db.GetTable<DbDays>().SingleOrDefault(d => d.Day == hours.Day)?.Id ?? 0,
                            Time_Open = hours.Time_Open,
                            Time_Closed = hours.Time_Closed
                        };
                        db.Insert(dbHours);
                    }
                }

                if (businessInfo.PhotoLinks != null)
                {
                    foreach (var link in businessInfo.PhotoLinks)
                    {
                        var dbPhotoLink = new DbPhotoLink
                        {
                            BusinessInfo_FK = insertedBusinessInfo.Id,
                            Link = link
                        };
                        db.Insert(dbPhotoLink);
                    }
                }

                return insertedBusinessInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting business information: " + ex.Message, ex);
            }

        }

        int IDBConnection.DeleteBusiness(int id)
        {
            try
            {
                return _connector.Delete(new DbBusinessInfo(id));

            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion

        #region Category

        IList<ICategory> IDBConnection.SelectAllCategories()
        {
            try
            {
                    var result = from infos in _connector.Categories select infos;
                    return result.ToList<ICategory>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Favorites
        int IDBConnection.CreateFavorite(int userId, int businessId)
        {
            try
            {
                    return _connector.Insert(new DbFavorites(userId, businessId));
            }
            catch
            {
                throw;
            }
        }

        int IDBConnection.DeleteFavorites(int userId, int businessId)
        {
            try
            {
                return _connector.Favorites.Where(
                    f => f.BusinessInfo_Fk == businessId
                    && f.User_FK == userId
                    ).Delete();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Liked 
        int IDBConnection.CreateLiked(int userId, int businessId)
        {
            try
            {
                    return _connector.Insert(new DbLiked(userId, businessId));
            }
            catch
            {
                throw;
            }
        }

        int IDBConnection.DeleteLiked(int userId, int businessId)
        {
            try
            {
               return _connector.Liked.Where(
                   f => f.BusinessInfo_Fk == businessId
                   && f.User_FK == userId
                   ).Delete();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Disliked
        int IDBConnection.CreatDisliked(int userId, int businessId)
        {
            try
            {
                    return _connector.Insert(new DbDisliked(userId, businessId));
            }
            catch
            {
                throw;
            }
        }

        int IDBConnection.DeleteDisliked(int userId, int businessId)
        {
            try
            {
               return  _connector.Disliked.Where(
                  f => f.BusinessInfo_Fk == businessId
                  && f.User_FK == userId
                  ).Delete();
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}