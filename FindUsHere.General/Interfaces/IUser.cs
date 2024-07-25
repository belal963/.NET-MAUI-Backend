using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.General.Interfaces
{
    public interface IUser
    {
        int Id { get; } 
        string UserName { get; }

        string Password { get; }

        string UserEmail { get; }

        List<int> Favorites { get; }
        List<int> Likeds { get;  }

        List<int> Dislikeds { get; }
    }
}
