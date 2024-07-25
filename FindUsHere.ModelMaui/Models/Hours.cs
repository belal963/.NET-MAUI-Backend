using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.ModelMaui.Models
{
    public class Hours
    {
        public Dictionary<TimeOnly, TimeOnly>? OpenFromTo { get; set; }
        public string? Day { get; set; }
    }
}
