namespace WPF_Projeto_BD.Models
{
    public class Cliente
    {
        public int Id { get; set; } // id_cliente no banco

        public string Nome { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

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
