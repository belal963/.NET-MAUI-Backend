using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.ModelMaui.Models
{
    public class FileUpload
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] File { get; set; }
    }
}
