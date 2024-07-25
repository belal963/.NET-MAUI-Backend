using FindUsHere.General.Interfaces;
using System;
using System.Collections.Generic;

namespace FindUsHere.RestApi.Model
{
    public class ResApiHours : IHours
    {
        public TimeSpan Time_Open { get; set; }
        public TimeSpan Time_Closed { get; set; }
        public string Day { get; set; }
    }
}
