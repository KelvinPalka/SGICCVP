using System; // Importa namespaces essenciais do C#
using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Controllers; // Importa o namespace que contém o ClienteController
using WPF_Projeto_BD.Models; // Importa o namespace que contém o modelo Cliente

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Classe que representa a janela de listagem de clientes
    public partial class ClienteLista : Window
    {
        private readonly ClienteController controller; // Referência ao controller de clientes

        // Construtor da classe, recebe o controller como dependência
        public ClienteLista(ClienteController controller)
        {
            InitializeComponent(); // Inicializa os componentes do XAML
            this.controller = controller; // Atribui a referência do controller recebido

            AtualizarLista(); // Atualiza a lista de clientes ao abrir a janela
        }

        // Método para atualizar o DataGrid com a lista de clientes
        private void AtualizarLista()
        {
            try
            {
                dgClientes.ItemsSource = null; // Limpa a fonte de dados antes de atualizar
                dgClientes.ItemsSource = controller.ObterListaClientes(); // Obtém lista do controller e atribui ao DataGrid
            }
            catch (Exception ex)
            {
                // Exibe uma mensagem de erro caso haja problema ao carregar a lista
                MessageBox.Show(
                    "Erro ao carregar lista de clientes: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // Evento do botão "Voltar" para retornar à tela inicial
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.VoltarHome(this); // Chama método do controller para voltar à tela principal
                Close(); // Fecha a janela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao voltar para a tela inicial: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // Evento para abrir a tela de cadastro de cliente
        private void Adicionar_Cliente(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.AbrirTelaCadastroCliente(); // Abre a tela de cadastro via controller
                Close(); // Fecha a janela de listagem
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao abrir cadastro de cliente: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // Evento para editar o cliente selecionado
        private void Editar_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente; // Obtém o cliente selecionado
            if (cliente == null)
            {
                // Exibe aviso caso nenhum cliente esteja selecionado
                MessageBox.Show(
                    "Selecione um cliente para editar.",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return; // Interrompe a execução
            }

            try
            {
                controller.AbrirTelaEdicaoCliente(cliente); // Abre tela de edição do cliente selecionado
                Close(); // Fecha a janela de listagem
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao abrir edição do cliente: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // Evento para excluir o cliente selecionado
        private void Excluir_Cliente(object sender, RoutedEventArgs e)
        {
            var cliente = dgClientes.SelectedItem as Cliente; // Obtém o cliente selecionado
            if (cliente == null)
            {
                // Exibe aviso caso nenhum cliente esteja selecionado
                MessageBox.Show(
                    "Selecione um cliente para excluir.",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return; // Interrompe a execução
            }

            try
            {
                controller.ExcluirCliente(cliente); // Chama método do controller para excluir cliente
                AtualizarLista(); // Atualiza o DataGrid após exclusão
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao excluir cliente: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}

/*
Resumo técnico:
- ClienteLista é a View responsável por exibir a lista de clientes em um DataGrid.
- Segue o padrão MVC + DAO: toda lógica de negócios é delegada ao ClienteController.
- Métodos:
    - AtualizarLista: atualiza o DataGrid com os clientes do banco.
    - BtnVoltar_Click: retorna à tela inicial.
    - Adicionar_Cliente: abre tela de cadastro de cliente.
    - Editar_Cliente: abre tela de edição do cliente selecionado.
    - Excluir_Cliente: exclui o cliente selecionado e atualiza a lista.
- Possui tratamento de exceções robusto com mensagens claras para o usuário.
- A interface é responsável apenas pela interação visual; nenhuma lógica de negócio ou acesso direto a banco ocorre aqui.
*/
