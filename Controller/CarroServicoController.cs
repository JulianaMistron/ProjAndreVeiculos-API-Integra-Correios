using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CarroServicoController
    {
        public int Id { get; set; }
        public CarroController Carro { get; set; }

        public ServicoController Servico { get; set; }
    }
}
