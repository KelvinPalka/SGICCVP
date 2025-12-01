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
using WPF_Projeto_BD.Controllers; // Importa o ClienteController
using WPF_Projeto_BD.Models; // Importa os modelos Usuario e Cliente

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela para cadastrar pedidos
    /// </summary>
    public partial class PedidoCadastro : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado

        // Construtor da tela de cadastro de pedidos
        public PedidoCadastro(Usuario usuario)
        {
            usuarioLogado = usuario; // Armazena o usuário logado
            InitializeComponent(); // Inicializa os componentes visuais
            CarregarClientes(); // Preenche o ComboBox com clientes da empresa
        }

        // Método que carrega a lista de clientes no ComboBox
        private void CarregarClientes()
        {
            var controller = new ClienteController(usuarioLogado); // Inicializa controller de clientes
            var lista = controller.ObterListaClientes(); // Obtém lista de clientes da empresa

            cbCliente.ItemsSource = lista; // Define fonte de dados do ComboBox
            cbCliente.DisplayMemberPath = "Nome"; // Mostra o nome do cliente
            cbCliente.SelectedValuePath = "Id"; // Armazena o ID do cliente selecionado
        }

        // Evento do botão "Cancelar", retorna à lista de pedidos
        private void Cancelar(object sender, RoutedEventArgs e)
        {
            var pedidosLista = new PedidosLista(usuarioLogado); // Cria a tela de listagem de pedidos
            pedidosLista.Show(); // Exibe a tela de pedidos
            this.Close(); // Fecha a tela de cadastro
        }

        // Evento do botão "Voltar", retorna para a tela Home
        private void Voltar(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuarioLogado); // Cria a tela Home
            home.Show(); // Exibe a tela Home
            this.Close(); // Fecha a tela de cadastro
        }
    }
}

/*
Resumo técnico:
- PedidoCadastro é a View responsável por cadastrar novos pedidos.
- Segue o padrão MVC: toda a lógica de obtenção de clientes é feita via ClienteController.
- Preenche um ComboBox com os clientes da empresa, exibindo o nome e armazenando o ID.
- Botões Cancelar e Voltar permitem navegação para PedidosLista ou Home, respectivamente.
- Nenhuma lógica de persistência de pedidos ocorre nesta View; é responsabilidade do controller correspondente.
*/
