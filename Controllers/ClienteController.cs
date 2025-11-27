//importações padrão
using System;
using System.Collections.Generic;
using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Controllers // Define o namespace da aplicação (Controllers)
{
    public class ClienteController 
    {
        private ClienteDAO dao = new ClienteDAO(); // Instancia o objeto DAO para operações de banco de dados

        // ---- REGRAS DE NEGÓCIO ----
        private string Validar(string nome, string cpf, string endereco, string telefone, string email)
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
                return "Email inválido.";

            return "OK";
        }

        // ---- CRUD ----

        public string Salvar(string nome, string cpf, string endereco, string telefone, string email)
        {
            var validar = Validar(nome, cpf, endereco, telefone, email);
            if (validar != "OK") return validar;

            var cliente = new Cliente(nome, cpf, endereco, telefone, email);
            dao.Inserir(cliente);

            return "OK";
        }

        public string Editar(Cliente cliente, string nome, string cpf, string endereco, string telefone, string email) 
        {
            var validar = Validar(nome, cpf, endereco, telefone, email);
            if (validar != "OK") return validar;

            cliente.Nome = nome;
            cliente.CPF_CNPJ = cpf;
            cliente.Endereco = endereco;
            cliente.Telefone = telefone;
            cliente.Email = email;

            dao.Editar(cliente);
            return "OK";
        }

        public void ExcluirCliente(Cliente c)
        {
            var confirm = MessageBox.Show(
                $"Excluir {c.Nome}?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirm == MessageBoxResult.Yes)
                dao.Excluir(c.Id);
        }

        // ---- LEITURA ----
        public List<Cliente> ObterListaClientes()
        {
            return dao.Listar();
        }

        // ---- NAVEGAÇÃO (TELAS) ----

        public void AbrirTelaCadastroCliente()
        {
            new ClienteCadastro(this).Show();
        }

        public void AbrirTelaEdicaoCliente(Cliente c)
        {
            new ClienteCadastro(c, this).Show();
        }

        public void VoltarHome()
        {
            new Home().Show();
        }
    }
}
