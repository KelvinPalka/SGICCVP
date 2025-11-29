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
    /// Lógica interna para PedidoCadastro.xaml
    /// </summary>
    public partial class PedidoCadastro : Window
    {
        private Usuario usuarioLogado;

        public PedidoCadastro(Usuario usuario)
        {
            usuarioLogado = usuario;
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            var controller = new ClienteController(usuarioLogado);
            var lista = controller.ObterListaClientes();

            cbCliente.ItemsSource = lista;
            cbCliente.DisplayMemberPath = "Nome";
            cbCliente.SelectedValuePath = "Id";
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            var pedidosLista = new PedidosLista(usuarioLogado);
            pedidosLista.Show();
            this.Close();
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuarioLogado);
            home.Show();
            this.Close();
        }
    }
}
