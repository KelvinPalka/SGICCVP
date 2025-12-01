using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Empresa // Classe que representa uma empresa no sistema
    {
        public int Id { get; set; } // Identificador único da empresa
        public string CNPJ { get; set; } // Número do CNPJ da empresa
        public string Nome_fantasia { get; set; } // Nome fantasia utilizado pela empresa
        public string Email { get; set; } // Email de contato da empresa
        public string Telefone { get; set; } // Telefone de contato da empresa
        public string Razao_social { get; set; } // Razão social da empresa
        public string Endereco { get; set; } // Endereço completo da empresa

        // Construtor vazio
        public Empresa() { }

        // Construtor parametrizado para criar uma empresa com todos os dados
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

/*
Resumo técnico:
- Empresa é um Model que representa os dados cadastrais de uma empresa no sistema.
- Contém informações essenciais como ID, CNPJ, nome fantasia, razão social, email, telefone e endereço.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de empresas, separando a lógica de negócios (Controller) e persistência (DAO).
*/
