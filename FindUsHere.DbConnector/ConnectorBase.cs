using FindUsHere.General.Interfaces;
using FindUsHere.DbConnector.Models;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;


namespace FindUsHere.DbConnector
{
    internal sealed class ConnectorBase : DataConnection
    {
        private static string connStr = @"Server=******;TrustServerCertificate=True;Database=*******;User Id=******;Password=********";

        private ConnectorBase() : base(ProviderName.SqlServer, connStr, MappingSchemas.Get()) { }

        private static ConnectorBase? _instance;

        public static ConnectorBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConnectorBase();
            }
            return _instance;
        }

        public ITable<DbBusinessInfo> BusinessInfo => this.GetTable<DbBusinessInfo>();
        public ITable<DbCategory> Categories => this.GetTable<DbCategory>();
        public ITable<DbFavorites> Favorites => this.GetTable<DbFavorites>();
        public ITable<DbLiked> Liked => this.GetTable<DbLiked>();
        public ITable<DbDisliked> Disliked => this.GetTable<DbDisliked>();
        public ITable<DbPhotoLink> PhotoLinks => this.GetTable<DbPhotoLink>();
        public ITable<DbUser> User => this.GetTable<DbUser>();
        public ITable<DbHours> Hours => this.GetTable<DbHours>();

        public ITable<DbDays> Days => this.GetTable<DbDays>();
    }
}
