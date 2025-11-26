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
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Models;    

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para PedidosLista.xaml
    /// </summary>
    public partial class PedidosLista : Window
    {
        public PedidosLista()
        {
            InitializeComponent();
            Carregar_Pedido(this, null);
        }

        private void Carregar_Pedido(object sender, RoutedEventArgs e)
        {
            var controller = new PedidoController();
            var pedidos = controller.GetPedidos();
            dgPedidos.ItemsSource = pedidos;
        }

        private void CadastrarPedido(object sender, RoutedEventArgs e)
        {
            var cadastrarPedidoWindow = new PedidoCadastro();
            cadastrarPedidoWindow.Show();
            this.Close();
        }
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home();
            home.Show();
            this.Close();
        }

    }
}
