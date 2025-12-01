using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Funcionario // Classe que representa um funcionário da empresa no sistema
    {
        public int Id { get; set; } // Identificador único do funcionário
        public string Nome { get; set; } // Nome completo do funcionário
        public string CPF { get; set; } // CPF do funcionário
        public string Cargo { get; set; } // Cargo ou função do funcionário
        public string Telefone { get; set; } // Telefone de contato do funcionário
        public string Email { get; set; } // Email de contato do funcionário
        public string Departamento { get; set; } // Departamento ao qual o funcionário pertence
        public int IdEmpresa { get; set; } // ID da empresa à qual o funcionário está vinculado

        // Construtor vazio
        public Funcionario() { }

        // Construtor parametrizado para criar um funcionário com todos os dados
        public Funcionario(int id, string nome, string cPF, string cargo, string telefone, string email, string departamento, int idEmpresa)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            Cargo = cargo;
            Telefone = telefone;
            Email = email;
            Departamento = departamento;
            IdEmpresa = idEmpresa;
        }
    }
}

/*
Resumo técnico:
- Funcionario é um Model que representa os funcionários da empresa no sistema.
- Contém informações essenciais como ID, nome, CPF, cargo, telefone, email, departamento e empresa vinculada.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de funcionários, separando a lógica de negócios (Controller) e persistência (DAO).
*/
