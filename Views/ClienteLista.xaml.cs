// Importações padrão
using System.Windows;
using WPF_Projeto_BD.Controllers; // Importa o namespace dos controllers (Contrllers)
using WPF_Projeto_BD.Models; // Importa o namespace dos modelos (Models)

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    public partial class ClienteLista : Window // Define a classe parcial ClienteLista que herda de Window
    {
        private readonly ClienteController controller; // Declara uma variável privada para o controller

        public ClienteLista(ClienteController controller) // Construtor da classe que recebe um ClienteController como parâmetro
        {
            InitializeComponent(); // Inicializa os componentes da interface gráfica
            this.controller = controller; // Atribui o controller recebido à variável privada

            dgClientes.ItemsSource = controller.ObterListaClientes(); // Define a fonte de dados do DataGrid com a lista de clientes obtida do controller
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e) // Evento de clique do botão "Voltar"
        {
            controller.VoltarHome(); // Chama o método do controller para voltar à tela inicial
            Close(); // Fecha a janela atual
        }

        private void Adicionar_Cliente(object sender, RoutedEventArgs e) // Evento de clique do botão "Adicionar Cliente"
        {
            controller.AbrirTelaCadastroCliente(); // Chama o método do controller para abrir a tela de cadastro de cliente
            Close(); // Fecha a janela atual
        }

        private void Editar_Cliente(object sender, RoutedEventArgs e) // Evento de clique do botão "Editar Cliente"
        {
            var cliente = dgClientes.SelectedItem as Cliente; // Obtém o cliente selecionado no DataGrid
            if (cliente == null) return; // Se nenhum cliente estiver selecionado, retorna

            controller.AbrirTelaEdicaoCliente(cliente); // Chama o método do controller para abrir a tela de edição do cliente selecionado
            Close(); // Fecha a janela atual
        }

        private void Excluir_Cliente(object sender, RoutedEventArgs e) // Evento de clique do botão "Excluir Cliente"
        {
            var cliente = dgClientes.SelectedItem as Cliente; // Obtém o cliente selecionado no DataGrid
            if (cliente == null) return; // Se nenhum cliente estiver selecionado, retorna

            controller.ExcluirCliente(cliente); // Chama o método do controller para excluir o cliente selecionado
            dgClientes.ItemsSource = controller.ObterListaClientes(); // Atualiza a fonte de dados do DataGrid com a lista atualizada de clientes
        }
    }
}
