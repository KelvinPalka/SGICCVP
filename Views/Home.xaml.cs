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
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Usuario usuarioLogado;
        public Home(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            VerificarPermissao();
        }

        private void VerificarPermissao()
        {
            if(usuarioLogado.TipoUsuario == "admin")
            {
                btnConfig.Visibility = Visibility.Visible;
            }
            else
            {
                btnConfig.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnEstoque_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            var pedidosWindow = new PedidosLista(usuarioLogado);
            pedidosWindow.Show();
            this.Close();
        }

        private void BtnProducao_Click(object sender, RoutedEventArgs e)
        {

        }    
        
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            var controller = new ClienteController(usuarioLogado);
            var ClientesWindow = new ClienteLista(controller);
            ClientesWindow.Show();
            this.Close();
        }



        private void BtnVendas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Tem certeza que deseja sair?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new Config(usuarioLogado);
            configWindow.Show();
            this.Close();
        }
    }
}
