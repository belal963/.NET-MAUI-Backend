using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.General.Interfaces
{
    public interface IBusinessInfo
    {

        int Id { get; }

        string Category { get; }

        public int UserId { get; }

        string Title { get; }

        string Description { get; }

        string PhoneNumber { get; }

        string Email { get; }

        string Website { get; }

        int PostCode { get; }

        string City { get; }

        string Street { get; }

        string HouseNumber { get; }

        string Addition { get; }

        float GpsLongitude { get; }

        float GpsLatitude { get; }

        List<IHours> Hours { get; }

        List<string> PhotoLinks { get; }

    }
}
