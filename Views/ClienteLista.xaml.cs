using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para ClienteLista.xaml
    /// </summary>
    public partial class ClienteLista : Window
    {
        public ClienteLista()
        {
            InitializeComponent();
            Carregar_Clientes(this, null);
        }

        private void Carregar_Clientes(object sender, RoutedEventArgs e)
        {
            var controller = new ClienteController();
            var clientes = controller.GetClientes();
            dgClientes.ItemsSource = clientes;
        }

        private void Adicionar_Cliente(object sender, RoutedEventArgs e)
        {
             var telaCadastro = new ClienteCadastro(); // cria nova tela
             telaCadastro.Show();                      // mostra
             this.Close();                             // fecha tela atual
        }

        private void Excluir_Cliente(object sender, RoutedEventArgs e)
        {
            var controller = new ClienteController();
            var clienteSelecionado = (Models.Cliente)dgClientes.SelectedItem;

            if (clienteSelecionado != null)
            {
                var confirm = MessageBox.Show(
                    $"Tem certeza que deseja excluir o cliente {clienteSelecionado.Nome}?" +
                    $"Esta ação NÃO poderá ser desfeita.",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirm == MessageBoxResult.No)
                    return;

                controller.ExcluirCliente(clienteSelecionado.Id);

                MessageBox.Show($"Cliente {clienteSelecionado.Nome} excluído com sucesso!");

                // Recarrega a lista
                Carregar_Clientes(this, null);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.");
            }
        }

        public void Editar_Cliente(object sender, RoutedEventArgs e)
        {
            var clienteSelecionado = (Models.Cliente)dgClientes.SelectedItem;
            if (clienteSelecionado != null)
            {
                var telaEdicao = new ClienteCadastro(clienteSelecionado); // cria nova tela com o cliente selecionado
                telaEdicao.Show();                                       // mostra
                this.Close();                                            // fecha tela atual
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para editar.");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home();
            home.Show();
            this.Close();
        }


    }
}
