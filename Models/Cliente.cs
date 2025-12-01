namespace WPF_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Cliente // Define a classe Cliente que representa a entidade no sistema
    {
        public int Id { get; set; } // Propriedade para armazenar o ID do cliente (chave primária)

        public string Nome { get; set; } // Propriedade para armazenar o nome do cliente
        public string CPF_CNPJ { get; set; } // Propriedade para armazenar CPF ou CNPJ do cliente
        public string Endereco { get; set; } // Propriedade para armazenar o endereço do cliente
        public string Telefone { get; set; } // Propriedade para armazenar o telefone do cliente
        public string Email { get; set; } // Propriedade para armazenar o e-mail do cliente

        // Construtor vazio obrigatório para DAO 
        public Cliente() { }

        // Construtor para criar instâncias do cliente com dados completos
        public Cliente(string nome, string cpf_cnpj, string endereco, string telefone, string email)
        {
            Nome = nome; // Inicializa a propriedade Nome
            CPF_CNPJ = cpf_cnpj; // Inicializa a propriedade CPF_CNPJ
            Endereco = endereco; // Inicializa a propriedade Endereco
            Telefone = telefone; // Inicializa a propriedade Telefone
            Email = email; // Inicializa a propriedade Email
        }
    }
}

/*
Resumo técnico:
- Cliente é uma classe de entidade (Model) que representa os clientes do sistema.
- Contém propriedades para ID, nome, CPF/CNPJ, endereço, telefone e e-mail.
- Possui construtor vazio necessário para que DAOs ou frameworks de ORM possam instanciar objetos dinamicamente.
- Possui construtor parametrizado para facilitar criação de objetos com todos os dados preenchidos.
- Centraliza a representação de dados do cliente, separando a lógica de negócios (Controller) e persistência (DAO).
*/
