using FindUsHere.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.ModelMaui.Models
{
    public class Category : ICategory
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
