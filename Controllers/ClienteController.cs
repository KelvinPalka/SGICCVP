//Importações padrão
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
// importando o DAO, Models e Views necessários
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Views;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Controllers // Definindo o namespace da aplicação
{
    public class ClienteController // Definindo a classe ClienteController
    {
        private readonly Usuario usuarioLogado; // Usuário logado
        private readonly ClienteDAO dao = new ClienteDAO(); // Instância do DAO para operações com Cliente

        public ClienteController(Usuario usuario) // Construtor que recebe o usuário logado
        {
            usuarioLogado = usuario;   // Atribui o usuário logado à variável da classe
        }

        // ============================
        // Validação de campos
        // ============================
        private string Validar(string nome, string cpf, string endereco, string telefone, string email, int? idIgnorar = null) // Validação dos campos do cliente / int? Variável inteira que pode ser nula; inicialmente sem valor definido)
        {
            if (string.IsNullOrWhiteSpace(nome)) // Verifica se o nome está vazio ou contém apenas espaços em branco
                return "O nome não pode estar vazio."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length < 11 || cpf.Length > 14) // Verifica se o CPF/CNPJ está vazio ou tem menos de 11 caracteres ou mais que 14
                return "CPF/CNPJ inválido."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(endereco)) // Verifica se o endereço está vazio ou contém apenas espaços em branco
                return "O endereço não pode estar vazio."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 8) // Verifica se o telefone está vazio ou tem menos de 8 caracteres
                return "Telefone inválido."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) // Verifica se o e-mail está vazio ou não contém o caractere "@"
                return "E-mail inválido."; // Retorna mensagem de erro

            // Verificar duplicidade no banco, ignorando o cliente atual
            var todos = dao.Listar(); // Obtém todos os clientes do banco de dados
            foreach (var c in todos) // Percorre cada cliente na lista
            {
                if (idIgnorar.HasValue && c.Id == idIgnorar.Value)  // Executa o bloco apenas se idIgnorar tiver valor e for igual a c.Id
                    continue; // ignora o próprio cliente

                if (c.CPF_CNPJ == cpf) // Verifica se o CPF/CNPJ já está cadastrado
                    return "Este CPF/CNPJ já está cadastrado."; // Retorna mensagem de erro

                if (c.Email == email) // Verifica se o e-mail já está cadastrado
                    return "Este e-mail já está cadastrado."; // Retorna mensagem de erro
            }

            return "OK"; // Retorna "OK" se todas as validações passarem
        }

        // ============================
        // Salvar novo cliente
        // ============================
        public string Salvar(string nome, string cpf, string endereco, string telefone, string email) // Método para salvar um novo cliente
        {
            try // Tenta executar o bloco de código
            {
                cpf = Regex.Replace(cpf, "[^0-9]", ""); // Remove caracteres não numéricos do CPF
                telefone = Regex.Replace(telefone, "[^0-9]", ""); // Remove caracteres não numéricos do telefone

                var validar = Validar(nome, cpf, endereco, telefone, email); // Valida os campos do cliente
                if (validar != "OK") return validar; // Retorna a mensagem de erro se a validação falhar

                var cliente = new Cliente(nome, cpf, endereco, telefone, email); // Cria uma nova instância de Cliente
                dao.Inserir(cliente); // Insere o cliente no banco de dados

                return "OK"; // Retorna "OK" se o cliente for salvo com sucesso
            }
            catch (Exception ex) // Captura qualquer exceção que ocorra durante o processo
            {
                return "Erro ao salvar cliente: " + ex.Message; // Retorna mensagem de erro com detalhes da exceção
            }
        }

        // ============================
        // Editar cliente existente
        // ============================
        public string Editar(Cliente cliente, string nome, string cpf, string endereco, string telefone, string email) // Método para editar um cliente existente
        {
            try // Tenta executar o bloco de código
            {
                cpf = Regex.Replace(cpf, "[^0-9]", ""); // Remove caracteres não numéricos do CPF
                telefone = Regex.Replace(telefone, "[^0-9]", ""); // Remove caracteres não numéricos do telefone

                var validar = Validar(nome, cpf, endereco, telefone, email, cliente.Id); // Valida os campos do cliente, ignorando o próprio cliente
                if (validar != "OK") return validar; // Retorna a mensagem de erro se a validação falhar

                cliente.Nome = nome; // Atualiza o nome do cliente
                cliente.CPF_CNPJ = cpf; // Atualiza o CPF/CNPJ do cliente
                cliente.Endereco = endereco; // Atualiza o endereço do cliente
                cliente.Telefone = telefone; // Atualiza o telefone do cliente
                cliente.Email = email; // Atualiza o e-mail do cliente

                dao.Editar(cliente); // Edita o cliente no banco de dados

                return "OK"; // Retorna "OK" se o cliente for editado com sucesso
            }
            catch (Exception ex) // Captura qualquer exceção que ocorra durante o processo
            { 
                return "Erro ao editar cliente: " + ex.Message; // Retorna mensagem de erro com detalhes da exceção
            }
        }

        // ============================
        // Excluir cliente
        // ============================
        public void ExcluirCliente(Cliente cliente) // Método para excluir um cliente
        {
            var confirm = MessageBox.Show( // Exibe uma mensagem de confirmação para o usuário
                $"Deseja realmente excluir {cliente.Nome}?", // Mensagem de confirmação
                "Confirmação", // Título da mensagem
                MessageBoxButton.YesNo, // Botões Sim e Não
                MessageBoxImage.Warning // Ícone de aviso
            );

            if (confirm == MessageBoxResult.Yes) // Se o usuário confirmar a exclusão
            {
                try //  Tenta executar o bloco de código
                { 
                    dao.Excluir(cliente.Id); // Exclui o cliente do banco de dados
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra durante o processo
                {
                    MessageBox.Show("Erro ao excluir cliente: " + ex.Message, // Exibe mensagem de erro
                        "Erro", MessageBoxButton.OK, MessageBoxImage.Error); // Título, botão e ícone da mensagem
                }
            }
        }

        // ============================
        // Obter lista de clientes
        // ============================
        public List<Cliente> ObterListaClientes() // Método para obter a lista de clientes
        {
            try // Tenta executar o bloco de código
            { 
                return dao.Listar(); // Retorna a lista de clientes do banco de dados
            }
            catch // Captura qualquer exceção que ocorra durante o processo
            {
                return new List<Cliente>(); // Retorna uma lista vazia em caso de erro
            }
        }

        // ============================
        // Navegação
        // ============================
        public void VoltarHome(Window telaAtual) // Método para voltar à tela inicial (Home)
        {
            if (usuarioLogado != null) // Verifica se o usuário logado não é nulo
            {
                new Home(usuarioLogado).Show(); // Abre a tela Home com o usuário logado
                telaAtual.Close(); // Fecha a tela atual
            }
            else // Se o usuário logado for nulo
            {
                MessageBox.Show("Usuário não definido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning); // Exibe mensagem de erro
            }
        }

        public void AbrirTelaCadastroCliente() // Método para abrir a tela de cadastro de cliente
        {
            new ClienteCadastro(this).Show(); // Abre a tela de cadastro de cliente, passando o controlador atual
        }

        public void AbrirTelaEdicaoCliente(Cliente cliente) // Método para abrir a tela de edição de cliente
        {
            new ClienteCadastro(cliente, this).Show(); // Abre a tela de edição de cliente, passando o cliente e o controlador atual
        }
    }
}

/*
ClienteController é responsável pelo gerenciamento da lógica de negócios relacionada a clientes no sistema.
- Realiza validação de campos, incluindo duplicidade de CPF/CNPJ e e-mail, permitindo ignorar o cliente atual durante edição.
- Executa operações CRUD (Inserir, Editar, Excluir, Listar) utilizando o ClienteDAO.
- Trata exceções e retorna mensagens de erro detalhadas para a View.
- Manipula a navegação entre telas (abrir cadastro, edição e Home) garantindo que o usuário logado seja considerado.
- Utiliza tipos nullable (int?) para lidar com identificadores opcionais durante validações.
- Integra o fluxo da aplicação garantindo consistência entre a interface e o banco de dados.
*/
