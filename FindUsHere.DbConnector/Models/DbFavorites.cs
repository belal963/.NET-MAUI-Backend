using LinqToDB.Mapping;

namespace FindUsHere.DbConnector.Models
{

    [Table("Favorites")]
    internal class DbFavorites 
    {

        #region DB Fields
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("BusinessInfo_FK"), NotNull]
        public int BusinessInfo_Fk { get; set; }

        [Column("User_FK"), NotNull]
        public int User_FK { get; set; }

        [Association(ThisKey =nameof(BusinessInfo_Fk), OtherKey = nameof(DbBusinessInfo.Id)) ]
        public DbBusinessInfo BusinessInfo { get; set; }


        [Association(ThisKey = nameof(User_FK), OtherKey = nameof(DbUser.Id))]
        public DbUser User { get; set; }
        #endregion

        #region constructor

        public DbFavorites() { }
        public DbFavorites(int id, int businessId)
        {
            User_FK = id;
            BusinessInfo_Fk = businessId;
        }

        #endregion
    }
}
