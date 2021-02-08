using System;

namespace Teste.EL.NucleoAluguel.API.Models
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Aniversario { get; set; }
        public EnderecoModel Endereco { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public ClienteModel() => Endereco = new EnderecoModel();
    }
}
