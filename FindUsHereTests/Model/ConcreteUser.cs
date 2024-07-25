using FindUsHere.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHereTests.Model
{
    public class ConcreteUser : IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public List<int> Favorites { get; set; }
        public List<int> Likeds { get; set; }
        public List<int> Dislikeds { get; set; }
    }
}
