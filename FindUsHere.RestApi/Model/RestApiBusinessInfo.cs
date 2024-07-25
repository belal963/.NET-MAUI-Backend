using FindUsHere.General.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FindUsHere.RestApi.Model
{
    public class RestApiBusinessInfo : IBusinessInfo
    {
        public int Id { get ; set; }
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
        public string Category { get; set; }
        public int UserId { get; set; }
        public List<ResApiHours> Hours { get; set; }
        public List<string> PhotoLinks { get; set; }

        List<IHours> IBusinessInfo.Hours => Hours?.Select(h => (IHours)h).ToList();
    }
}
