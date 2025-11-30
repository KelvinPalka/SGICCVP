using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_Projeto_BD.Data.DAO;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Views;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Controllers
{
    public class ClienteController
    {
        private readonly Usuario usuarioLogado;
        private readonly ClienteDAO dao = new ClienteDAO();

        public ClienteController(Usuario usuario)
        {
            usuarioLogado = usuario;
        }

        // ============================
        // Validação de campos
        // ============================
        private string Validar(string nome, string cpf, string endereco, string telefone, string email, int? idIgnorar = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return "O nome não pode estar vazio.";

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length < 11)
                return "CPF/CNPJ inválido.";

            if (string.IsNullOrWhiteSpace(endereco))
                return "O endereço não pode estar vazio.";

            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 8)
                return "Telefone inválido.";

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return "E-mail inválido.";

            // Verificar duplicidade no banco, ignorando o cliente atual
            var todos = dao.Listar();
            foreach (var c in todos)
            {
                if (idIgnorar.HasValue && c.Id == idIgnorar.Value)
                    continue; // ignora o próprio cliente

                if (c.CPF_CNPJ == cpf)
                    return "Este CPF/CNPJ já está cadastrado.";

                if (c.Email == email)
                    return "Este e-mail já está cadastrado.";
            }

            return "OK";
        }

        // ============================
        // Salvar novo cliente
        // ============================
        public string Salvar(string nome, string cpf, string endereco, string telefone, string email)
        {
            try
            {
                cpf = Regex.Replace(cpf, "[^0-9]", "");
                telefone = Regex.Replace(telefone, "[^0-9]", "");

                var validar = Validar(nome, cpf, endereco, telefone, email);
                if (validar != "OK") return validar;

                var cliente = new Cliente(nome, cpf, endereco, telefone, email);
                dao.Inserir(cliente);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Erro ao salvar cliente: " + ex.Message;
            }
        }

        // ============================
        // Editar cliente existente
        // ============================
        public string Editar(Cliente cliente, string nome, string cpf, string endereco, string telefone, string email)
        {
            try
            {
                cpf = Regex.Replace(cpf, "[^0-9]", "");
                telefone = Regex.Replace(telefone, "[^0-9]", "");

                var validar = Validar(nome, cpf, endereco, telefone, email, cliente.Id);
                if (validar != "OK") return validar;

                cliente.Nome = nome;
                cliente.CPF_CNPJ = cpf;
                cliente.Endereco = endereco;
                cliente.Telefone = telefone;
                cliente.Email = email;

                dao.Editar(cliente);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Erro ao editar cliente: " + ex.Message;
            }
        }

        // ============================
        // Excluir cliente
        // ============================
        public void ExcluirCliente(Cliente cliente)
        {
            var confirm = MessageBox.Show(
                $"Deseja realmente excluir {cliente.Nome}?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    dao.Excluir(cliente.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir cliente: " + ex.Message,
                        "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // ============================
        // Obter lista de clientes
        // ============================
        public List<Cliente> ObterListaClientes()
        {
            try
            {
                return dao.Listar();
            }
            catch
            {
                return new List<Cliente>();
            }
        }

        // ============================
        // Navegação
        // ============================
        public void VoltarHome(Window telaAtual)
        {
            if (usuarioLogado != null)
            {
                new Home(usuarioLogado).Show();
                telaAtual.Close();
            }
            else
            {
                MessageBox.Show("Usuário não definido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void AbrirTelaCadastroCliente()
        {
            new ClienteCadastro(this).Show();
        }

        public void AbrirTelaEdicaoCliente(Cliente cliente)
        {
            new ClienteCadastro(cliente, this).Show();
        }
    }
}
