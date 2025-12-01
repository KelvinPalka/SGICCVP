using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using Wpf_Projeto_BD.Models; // Importa os modelos Funcionario e Usuario
using WPF_Projeto_BD.Controllers; // Importa o controller FuncionarioController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela que exibe a lista de funcionários da empresa
    /// </summary>
    public partial class FuncionarioLista : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado
        private FuncionarioController controller; // Controller responsável pela lógica de funcionários

        // Construtor da tela, recebe o usuário logado
        public FuncionarioLista(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            controller = new FuncionarioController(); // Inicializa o controller
            usuarioLogado = usuario; // Armazena o usuário logado

            CarregarFuncionarios(); // Preenche o DataGrid com os funcionários da empresa
        }

        // =========================
        // Carregar lista de funcionários
        // =========================
        private void CarregarFuncionarios()
        {
            // Obtém todos os funcionários da empresa e atribui ao DataGrid
            dgFuncionarios.ItemsSource = controller.ObterTodos(usuarioLogado.IdEmpresa);
        }

        // =========================
        // Botão Adicionar Funcionário
        // =========================
        private void Adicionar_Funcionario(object sender, RoutedEventArgs e)
        {
            // Abre a tela de cadastro de funcionário
            var cadastrar = new FuncionarioCadastro(usuarioLogado);
            cadastrar.Show();
            this.Close(); // Fecha a tela de listagem
        }

        // =========================
        // Botão Editar Funcionário
        // =========================
        private void Editar_Funcionario(object sender, RoutedEventArgs e)
        {
            // Verifica se algum funcionário foi selecionado
            if (dgFuncionarios.SelectedItem is Funcionario funcionarioSelecionado)
            {
                // Abre a tela de edição passando o funcionário selecionado
                var editarWindow = new FuncionarioCadastro(usuarioLogado, funcionarioSelecionado);
                editarWindow.Show();
                this.Close(); // Fecha a tela de listagem
            }
        }

        // =========================
        // Botão Excluir Funcionário
        // =========================
        private void Excluir_Funcionario(object sender, RoutedEventArgs e)
        {
            // Verifica se algum funcionário foi selecionado
            if (dgFuncionarios.SelectedItem is Funcionario f)
            {
                // Confirmação antes de excluir
                var result = MessageBox.Show($"Deseja excluir o funcionário {f.Nome}?", "Confirmação", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    controller.ExcluirFuncionario(f.Id); // Chama o controller para excluir
                    CarregarFuncionarios(); // Atualiza a lista
                }
            }
        }

        // =========================
        // Botão Voltar
        // =========================
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuarioLogado); // Abre a tela inicial/home
            home.Show();
            this.Close(); // Fecha a tela de listagem
        }
    }
}

/*
Resumo técnico:
- FuncionarioLista é a View responsável por exibir a lista de funcionários da empresa.
- Segue o padrão MVC + DAO: toda lógica de obtenção e exclusão de dados é delegada ao FuncionarioController.
- Permite adicionar, editar, excluir e visualizar funcionários.
- Confirmação é solicitada antes da exclusão para evitar remoções acidentais.
- O DataGrid é atualizado automaticamente após qualquer alteração.
- Botão Voltar retorna à tela inicial.
- Nenhuma lógica de negócio ou acesso direto ao banco ocorre na View; tudo é tratado pelo controller.
*/
