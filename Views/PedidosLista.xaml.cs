using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; // Necessário para classes de interface (Window, RoutedEventArgs)
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Projeto_BD.Controllers; // Importa o PedidoController
using WPF_Projeto_BD.Data.DAO; // Importa DAOs se necessário
using WPF_Projeto_BD.Models; // Importa os modelos Usuario e Pedido

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela que exibe a lista de pedidos da empresa
    /// </summary>
    public partial class PedidosLista : Window
    {
        private Usuario usuariologado; // Usuário atualmente logado

        // Construtor da tela, recebe o usuário logado
        public PedidosLista(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuariologado = usuario; // Armazena o usuário logado
            Carregar_Pedido(this, null); // Carrega os pedidos ao abrir a tela
        }

        // Método que carrega os pedidos no DataGrid
        private void Carregar_Pedido(object sender, RoutedEventArgs e)
        {
            var controller = new PedidoController(); // Inicializa o controller de pedidos
            var pedidos = controller.GetPedidos(); // Obtém a lista de pedidos
            dgPedidos.ItemsSource = pedidos; // Atribui a lista ao DataGrid
        }

        // Evento do botão "Cadastrar Pedido"
        private void CadastrarPedido(object sender, RoutedEventArgs e)
        {
            var cadastrarPedidoWindow = new PedidoCadastro(usuariologado); // Abre a tela de cadastro de pedidos
            cadastrarPedidoWindow.Show();
            this.Close(); // Fecha a tela de listagem
        }

        // Evento do botão "Voltar"
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuariologado); // Cria a tela Home
            home.Show(); // Exibe a tela Home
            this.Close(); // Fecha a tela de listagem de pedidos
        }
    }
}

/*
Resumo técnico:
- PedidosLista é a View responsável por exibir todos os pedidos da empresa.
- Segue o padrão MVC: toda lógica de obtenção de dados é delegada ao PedidoController.
- Carregar_Pedido preenche o DataGrid com os pedidos existentes.
- Botão Cadastrar abre a tela PedidoCadastro para adicionar novos pedidos.
- Botão Voltar retorna à tela Home.
- Nenhuma lógica de persistência ou manipulação direta de pedidos ocorre na View; tudo é tratado pelo controller.
*/
