using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PagamentoController
    {
        public int Id { get; set; }
        public CartaoController Cartao { get; set; }
        public Boleto Boleto { get; set; }
        public PixController Pix { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
