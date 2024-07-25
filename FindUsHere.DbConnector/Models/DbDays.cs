using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.DbConnector.Models
{
    #region DB Fileds
    [LinqToDB.Mapping.Table("Days")]
    internal class DbDays :IDays
    {

        [PrimaryKey, Identity]

        public int Id { get; set; }

        [Column("Day"), NotNull]

        public string Day { get; set; }

        #endregion


        #region constructor
        public DbDays()
        {
            
        }

        #endregion
    }
}
