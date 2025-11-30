using System;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class ClienteCadastro : Window
    {
        private readonly ClienteController _controller;
        private Cliente clienteEditando;

        public ClienteCadastro(ClienteController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        public ClienteCadastro(Cliente cliente, ClienteController controller)
        {
            InitializeComponent();
            _controller = controller;
            clienteEditando = cliente;

            txtNome.Text = cliente.Nome;
            txtCPF_CNPJ.Text = cliente.CPF_CNPJ;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;

            btnSalvar.Content = "Atualizar Cliente";
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string cpf = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", "");
            string end = txtEndereco.Text.Trim();
            string tel = Regex.Replace(txtTelefone.Text, "[^0-9]", "");
            string email = txtEmail.Text.Trim();

            string resposta;

            try
            {
                if (clienteEditando == null)
                    resposta = _controller.Salvar(nome, cpf, end, tel, email);
                else
                    resposta = _controller.Editar(clienteEditando, nome, cpf, end, tel, email);

                if (resposta != "OK")
                {
                    MessageBox.Show(resposta, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                new ClienteLista(_controller).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
