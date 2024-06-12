using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PixController
    {
        public int Id { get; set; }
        public TipoPixController Tipo { get; set; }
        public string ChavePix { get; set; }
    }
}
