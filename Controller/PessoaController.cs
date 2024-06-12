using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class PessoaController
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public EnderecoController Endereco { get; set; }

        public string Telefone   { get; set; }

        public string Email { get; set; }

    }
}
