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

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para PedidoCadastro.xaml
    /// </summary>
    public partial class PedidoCadastro : Window
    {
        public PedidoCadastro()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            var controller = new ClienteController();
            var lista = controller.GetClientesSimples();

            cbCliente.ItemsSource = lista;
            cbCliente.DisplayMemberPath = "Nome";
            cbCliente.SelectedValuePath = "Id";
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            var pedidosLista = new PedidosLista();
            pedidosLista.Show();
            this.Close();
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            var home = new Home();
            home.Show();
            this.Close();
        }
    }
}
