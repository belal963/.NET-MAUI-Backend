using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.DbConnector.Models
{

    [Table("PhotoLink")]
    internal class DbPhotoLink : IPhotoLink
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("BusinessInfo_Fk"), NotNull]
        public int BusinessInfo_FK { get; set; }

        [Column("Link"), NotNull]
        public string Link { get; set; }

        [Association(ThisKey = nameof(BusinessInfo_FK), OtherKey = nameof(DbBusinessInfo.Id))]
        public DbBusinessInfo BusinessInfo { get; set; }
        #endregion

        #region constructor

        public DbPhotoLink()
        {
            
        }
        //public DbPhotoLink(string link , int id)
        //{
        //    Link = link;
        //    BusinessInfo = new DbBusinessInfo(id);
        //}
        #endregion


    }
}
