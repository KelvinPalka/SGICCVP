using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Fornecedor
    {
        public string Id {  get; set; }
        public string CPF_CNPJ { get; set; }
        public string Nome { get; set; }
        public string Email {  get; set; }
        public string Telefone { get; set; }

        public Fornecedor() { }

        public Fornecedor(string id, string cpf_cnpj, string nome, string email, string telefone)
        {
            Id = id;
            CPF_CNPJ = cpf_cnpj;
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }
    }
}
