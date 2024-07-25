using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.DbConnector.Models
{

    [Table("Liked")]
    internal class DbLiked 
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("BusinessInfo_Fk"), NotNull]
        public int BusinessInfo_Fk { get; set; }

        [Column("User_FK"), NotNull]
        public int User_FK { get; set; }

        [Association(ThisKey = nameof(BusinessInfo_Fk), OtherKey = nameof(DbBusinessInfo.Id))]
        public IBusinessInfo BusinessInfo { get; set; }


        [Association(ThisKey = nameof(User_FK), OtherKey = nameof(DbUser.Id))]
        public IUser User { get; set; }
        #endregion


        #region constructor

        public DbLiked() {}

        public DbLiked(int id, int businessId)
        {
            User_FK = id;
            BusinessInfo_Fk = businessId;
        }

        #endregion
    }
}
