using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Fornecedor // Classe que representa um fornecedor no sistema
    {
        public string Id { get; set; } // Identificador único do fornecedor
        public string CPF_CNPJ { get; set; } // CPF ou CNPJ do fornecedor
        public string Nome { get; set; } // Nome ou razão social do fornecedor
        public string Email { get; set; } // Email de contato do fornecedor
        public string Telefone { get; set; } // Telefone de contato do fornecedor

        // Construtor vazio
        public Fornecedor() { }

        // Construtor parametrizado para criar um fornecedor com todos os dados
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

/*
Resumo técnico:
- Fornecedor é um Model que representa os fornecedores da empresa no sistema.
- Contém informações essenciais como ID, CPF/CNPJ, nome, email e telefone.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de fornecedores, separando a lógica de negócios (Controller) e persistência (DAO).
*/
