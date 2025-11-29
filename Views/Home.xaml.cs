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
    /// Lógica interna para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void BtnEstoque_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            var pedidosWindow = new PedidosLista();
            pedidosWindow.Show();
            this.Close();
        }

        private void BtnProducao_Click(object sender, RoutedEventArgs e)
        {

        }    
        
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            var controller = new ClienteController();
            var ClientesWindow = new ClienteLista(controller);
            ClientesWindow.Show();
            this.Close();
        }



        private void BtnVendas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
