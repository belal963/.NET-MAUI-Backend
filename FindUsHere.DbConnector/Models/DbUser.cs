using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace FindUsHere.DbConnector.Models
{

    [Table("User")]
    internal class DbUser : IUser
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id;

        [Column("UserName"), NotNull]
        public string UserName;

        [Column("UserEmail"), NotNull]
        public string UserEmail;

        [Column("Password"), NotNull]
        public string Password;
        #endregion


        #region lists
        [Association(ThisKey = nameof(Id), OtherKey = nameof(DbFavorites.User_FK))]
        public List<DbFavorites> Favorites { get; set; } = new();

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DbLiked.User_FK))]
        public List<DbLiked> Likeds { get; set; } = new();

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DbDisliked.User_FK))]
        public List<DbDisliked> Dislikeds { get; set; } = new();

        #endregion

        #region intf

        int IUser.Id => Id;

        string IUser.UserName => UserName;

        string IUser.UserEmail => UserEmail;

        string IUser.Password => Password;

        List<int> IUser.Favorites => Favorites.Select(x=> x.BusinessInfo_Fk).ToList();

        List<int> IUser.Likeds => Likeds.Select(x=> x.BusinessInfo_Fk).ToList();

        List<int> IUser.Dislikeds => Dislikeds.Select(x => x.BusinessInfo_Fk).ToList();

        #endregion

        #region constructor

        public DbUser() 
        {
           
        }

        public DbUser(string userName)
        {
            UserName = userName;   
        }

        public DbUser(int id) 
        {
            Id = id;
        }
        public DbUser(string userName, string email, string password)
        {
            UserName = userName;
            Password = password;
            UserEmail = email;
        }

        #endregion
    }
}
