using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompraController
    {
        public int Id { get; set; }
        public CarroController Carro { get; set; }
        public Decimal Preco { get; set; }
        public DateTime DataCompra { get; set; }

    }
}