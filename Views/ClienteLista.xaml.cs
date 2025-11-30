using System;
using System.Windows;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class ClienteLista : Window
    {
        private readonly ClienteController controller;

        public ClienteLista(ClienteController controller)
        {
            InitializeComponent();
            this.controller = controller;

            AtualizarLista();
        }

        private void AtualizarLista()
        {
            try
            {
                dgClientes.ItemsSource = null;
                dgClientes.ItemsSource = controller.ObterListaClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lista de clientes: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.VoltarHome(this);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao voltar para a tela inicial: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Adicionar_Cliente(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.AbrirTelaCadastroCliente();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir cadastro de cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Editar_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente;
            if (cliente == null)
            {
                MessageBox.Show("Selecione um cliente para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                controller.AbrirTelaEdicaoCliente(cliente);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir edição do cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Excluir_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente;
            if (cliente == null)
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                controller.ExcluirCliente(cliente);
                AtualizarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
