using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projeto_BD.Models
{
    public class Empresa
    {
        public int Id {get; set;}
        public string CNPJ {get; set;}
        public string Nome_fantasia {get; set;}
        public string Email {get; set;}
        public string Telefone {get; set;}
        public string Razao_social {get; set;}
        public string Endereco {get; set;}

        public Empresa() { }

        public Empresa(int id, string cnpj, string nome_fantasia, string email, string telefone, string razao_social, string endereco)
        {
            Id = id;
            CNPJ = cnpj;
            Nome_fantasia = nome_fantasia;
            Email = email;
            Telefone = telefone;
            Razao_social = razao_social;
            Endereco = endereco;
        }
    }
}
