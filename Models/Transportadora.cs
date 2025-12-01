using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Transportadora // Classe que representa uma transportadora no sistema
    {
        public int Id { get; set; } // Identificador único da transportadora
        public string CNPJ { get; set; } // CNPJ da transportadora
        public string Nome_fantasia { get; set; } // Nome fantasia da transportadora
        public string Email { get; set; } // Email de contato da transportadora
        public string Telefone { get; set; } // Telefone de contato da transportadora
        public string Razao_social { get; set; } // Razão social da transportadora
        public string Endereco { get; set; } // Endereço completo da transportadora

        // Construtor vazio
        public Transportadora() { }

        // Construtor parametrizado para criar uma transportadora com todos os dados
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

/*
Resumo técnico:
- Transportadora é um Model que representa as transportadoras cadastradas no sistema.
- Contém informações essenciais como ID, CNPJ, nome fantasia, razão social, email, telefone e endereço.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de transportadoras, separando a lógica de negócios (Controller) e persistência (DAO).
*/
