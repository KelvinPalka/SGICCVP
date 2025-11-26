using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome {  get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Funcionario() { }

        public Funcionario(int id, string nome, string cPF, string cargo, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            Cargo = cargo;
            Telefone = telefone;
            Email = email;
        }
    }
}
