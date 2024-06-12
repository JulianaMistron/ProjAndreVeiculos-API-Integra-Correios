using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class VendaController
    {
        public int Id { get; set; }
        public CarroController Carro { get; set; }
        public DateTime DataVenda { get; set; }
        public Decimal ValorVenda { get; set; }
        public ClienteController Cliente { get; set; }
        public FuncionarioController Funcionario { get; set; }
        public PagamentoController Pagamento { get; set; }

    }
}
