using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.General.Interfaces
{
    public interface IPhotoLink
    {
        int BusinessInfo_FK {  get; set; }
        string Link { get; set; }

    }
}
