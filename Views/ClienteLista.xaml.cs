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

            dgClientes.ItemsSource = controller.ObterListaClientes();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            controller.VoltarHome();
            Close();
        }

        private void Adicionar_Cliente(object sender, RoutedEventArgs e)
        {
            controller.AbrirTelaCadastroCliente();
            Close();
        }

        private void Editar_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente;
            if (cliente == null) return;

            controller.AbrirTelaEdicaoCliente(cliente);
            Close();
        }

        private void Excluir_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente;
            if (cliente == null) return;

            controller.ExcluirCliente(cliente);
            dgClientes.ItemsSource = controller.ObterListaClientes();
        }
    }
}
