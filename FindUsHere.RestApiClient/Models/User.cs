using FindUsHere.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.Model.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public List<IFavorites> Favorites { get; set; }
        public List<ILiked> Likeds { get; set; }
        public List<IDisliked> Dislikeds { get; set; }
    }
}
