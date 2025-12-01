using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Models; // Importa o modelo Usuario
using WPF_Projeto_BD.Controllers; // Importa o controller UsuarioController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela que exibe a lista de usuários da empresa
    /// </summary>
    public partial class UsuarioLista : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado
        private UsuarioController controller; // Controller responsável pela lógica de usuários

        // Construtor da tela, recebe o usuário logado
        public UsuarioLista(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
            controller = new UsuarioController(); // Inicializa o controller

            CarregarUsuarios(); // Preenche o DataGrid com os usuários da empresa
        }

        // ================== Carregar Usuários da empresa ==================
        private void CarregarUsuarios()
        {
            var usuarios = controller.ObterTodos(usuarioLogado.IdEmpresa); // Obtém todos os usuários da empresa
            dgUsuarios.ItemsSource = usuarios; // Atribui a lista ao DataGrid
        }

        // ================== Editar Usuário ==================
        private void Editar_Usuario(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSelecionado)
            {
                // Abre a tela de cadastro passando o usuário selecionado para edição
                var telaEdicao = new UsuarioCadastro(usuarioLogado, usuarioSelecionado);
                telaEdicao.Show();
                this.Close(); // Fecha a tela de listagem
            }
        }

        // ================== Excluir Usuário ==================
        private void Excluir_Usuario(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSelecionado)
            {
                // Confirmação antes de excluir
                var result = MessageBox.Show(
                    $"Deseja realmente excluir o usuário {usuarioSelecionado.Nome}?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool excluido = controller.ExcluirUsuario(usuarioSelecionado.IdUsuario); // Chama controller para excluir
                    if (excluido)
                    {
                        MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CarregarUsuarios(); // Atualiza a lista
                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir o usuário.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // ================== Exportar Lista ==================
        private void BtnExportarLista_Click(object sender, RoutedEventArgs e)
        {
            // Chama o método GerarPDF no controller
            controller.GerarPDF();

            // Exibe mensagem de sucesso
            MessageBox.Show("PDF gerado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ================== Voltar para Home ==================
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Config(usuarioLogado); // Volta para a tela de configurações
            home.Show(); // Exibe a tela Home
            this.Close(); // Fecha a tela de listagem de usuários
        }
    }
}

/*
Resumo técnico:
- UsuarioLista é a View responsável por exibir todos os usuários da empresa.
- Segue o padrão MVC + DAO: toda lógica de obtenção, exclusão e exportação de dados é delegada ao UsuarioController.
- Permite editar, excluir e exportar a lista de usuários em PDF.
- Confirmação é solicitada antes da exclusão para evitar remoções acidentais.
- Botão Voltar retorna à tela Home.
- Nenhuma lógica de negócio ou acesso direto ao banco ocorre na View; tudo é tratado pelo controller.
*/
