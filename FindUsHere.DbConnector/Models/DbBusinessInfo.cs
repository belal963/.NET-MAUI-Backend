using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace FindUsHere.DbConnector.Models
{
    [Table("BusinessInfo")]
    internal class DbBusinessInfo : IBusinessInfo
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id;

        [Column("Category_FK"), NotNull]
        public int Category_FK;

        [Column("User_FK"), NotNull]
        public int User_FK;

        [Column("Title"), NotNull]
        public string Title;

        [Column("Description"), NotNull]
        public string Description;

        [Column("PhoneNumber"), NotNull]
        public string PhoneNumber;

        [Column("Email"), NotNull]
        public string Email;

        [Column("Website")]
        public string Website;

        [Column("PostCode"), NotNull]
        public int PostCode;

        [Column("City"), NotNull]
        public string City;

        [Column("Street"), NotNull]
        public string Street;

        [Column("HouseNumber"), NotNull]
        public string HouseNumber;

        [Column("Addition")]
        public string Addition;

        [Column("GpsLongitude"), NotNull]
        public float GpsLongitude;

        [Column("GpsLatitude"), NotNull]
        public float GpsLatitude;

        [Association(ThisKey = nameof(Category_FK), OtherKey = nameof(DbCategory.Id))]
        public DbCategory Category;

        [Association(ThisKey = nameof(User_FK), OtherKey = nameof(DbUser.Id))]
        public DbUser User;
        #endregion


        #region lists
        [Association(ThisKey = nameof(Id), OtherKey = nameof(DbHours.BusinessInfo_FK))]
        public List<DbHours> Hours { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DbPhotoLink.BusinessInfo_FK))]
        public List<DbPhotoLink> PhotoLinks { get; set; }
        #endregion


        #region intf

        int IBusinessInfo.Id => Id;

        string IBusinessInfo.Category => Category.Name;

        int IBusinessInfo.UserId => User.Id;

        string IBusinessInfo.Title => Title;

        string IBusinessInfo.Description => Description;

        string IBusinessInfo.PhoneNumber => PhoneNumber;

        string IBusinessInfo.Email => Email;

        string IBusinessInfo.Website => Website;

        int IBusinessInfo.PostCode => PostCode;

        string IBusinessInfo.City => City;

        string IBusinessInfo.Street => Street;

        string IBusinessInfo.HouseNumber => HouseNumber;

        string IBusinessInfo.Addition => Addition;

        float IBusinessInfo.GpsLongitude => GpsLongitude;

        float IBusinessInfo.GpsLatitude => GpsLatitude;

        List<IHours> IBusinessInfo.Hours => Hours.Select(x => (IHours)x).ToList();
        List<string> IBusinessInfo.PhotoLinks => PhotoLinks.Select(x => x.Link).ToList();
        #endregion


        #region constructor
        public DbBusinessInfo() {}
        public DbBusinessInfo(int id)
        {
            Id = id;
        }

        public DbBusinessInfo(int categoryId, int userId, string title, string description, string phoneNumber, string website, string email,
            string cebsite,string city, int postCode, string street, string houseNumber, string addition,
            float gpsLongitude, float gpsLatitude)
        {
            Category_FK = categoryId;
            User_FK = userId;
            Title = title;
            Description = description;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            PostCode = postCode;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Addition = addition;
            GpsLongitude = gpsLongitude;
            GpsLatitude = gpsLatitude;
        }
        #endregion
    }
}
