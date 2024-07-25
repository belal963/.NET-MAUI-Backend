using FindUsHere.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.Model.Models
{
    public class BusinessInfo : IBusinessInfo
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Addition { get; set; }
        public float GpsLongitude { get; set; }
        public float GpsLatitude { get; set; }
        public ICategory Category { get; set; }
        public IUser User { get; set; }
        public List<IHours> Hours { get; set; }
        public List<IPhotoLink> PhotoLinks { get; set; }
    }
}
