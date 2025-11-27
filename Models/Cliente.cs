namespace WPF_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Cliente // Define a classe Cliente
    {
        public int Id { get; set; } // Propriedade para o ID do cliente

        public string Nome { get; set; } // Propriedade para o nome do cliente
        public string CPF_CNPJ { get; set; } // Propriedade para o CPF ou CNPJ do cliente
        public string Endereco { get; set; } // Propriedade para o endereço do cliente
        public string Telefone { get; set; } // Propriedade para o telefone do cliente
        public string Email { get; set; } // Propriedade para o email do cliente

        // Construtor vazio obrigatório para DAO
        public Cliente() { }

        // Construtor para uso na aplicação
        public Cliente(string nome, string cpf_cnpj, string endereco, string telefone, string email) 
        {
            Nome = nome;
            CPF_CNPJ = cpf_cnpj;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
        }
    }
}
