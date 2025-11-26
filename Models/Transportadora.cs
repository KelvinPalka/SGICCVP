using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Transportadora
    {

        public int Id {  get; set; }
        public string CNPJ {  get;  set; }
        public string Nome_fantasia { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Razao_social { get; set; }
        public string Endereco { get; set; }

        public Transportadora() { }

        public Transportadora(int id, string cNPJ, string nome_fantasia, string email, string telefone, string razao_social, string endereco)
        {
            Id = id;
            CNPJ = cNPJ;
            Nome_fantasia = nome_fantasia;
            Email = email;
            Telefone = telefone;
            Razao_social = razao_social;
            Endereco = endereco;
        }
    }
}
