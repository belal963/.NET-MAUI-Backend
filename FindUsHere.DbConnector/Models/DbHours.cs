using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System;

namespace FindUsHere.DbConnector.Models
{
    [Table("Hours")]
    internal class DbHours : IHours 
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id;

        [Column("BusinessInfo_FK"), NotNull]
        public int BusinessInfo_FK;

        [Column("Days_FK"), NotNull]
        public int Days_FK;

        [Column("Time_Open"), NotNull]
        public TimeSpan Time_Open;

        [Column("Time_Closed"), NotNull]
        public TimeSpan Time_Closed;

        [Association(ThisKey = nameof(Days_FK), OtherKey = nameof(DbDays.Id))]
        public DbDays Days;

        [Association(ThisKey = nameof(BusinessInfo_FK), OtherKey = nameof(DbBusinessInfo.Id))]
        public DbBusinessInfo BusinessInfo;
        #endregion

        #region intf
        string IHours.Day => Days.Day;

        TimeSpan IHours.Time_Open => Time_Open;

        TimeSpan IHours.Time_Closed => Time_Closed;
        #endregion


        #region constructor
        public DbHours() {}

        public DbHours(int businessInfoId, TimeSpan open, TimeSpan closed)
        {
            BusinessInfo_FK = businessInfoId;
            Time_Open = open;
            Time_Closed = closed;
        }
        #endregion
    }
}
