using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para UsuarioLista.xaml
    /// </summary>
    public partial class UsuarioLista : Window
    {
        private Usuario usuarioLogado;
        private UsuarioController controller;

        public UsuarioLista(Usuario usuario)
        {
            InitializeComponent();

            usuarioLogado = usuario;
            controller = new UsuarioController();

            CarregarUsuarios();
        }

        // ================== Carregar Usuários da empresa ==================
        private void CarregarUsuarios()
        {
            var usuarios = controller.ObterTodos(usuarioLogado.IdEmpresa);
            dgUsuarios.ItemsSource = usuarios;
        }

        // ================== Editar Usuário ==================
        private void Editar_Usuario(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSelecionado)
            {
                var telaEdicao = new UsuarioCadastro(usuarioLogado, usuarioSelecionado);
                telaEdicao.Show();
                this.Close();
            }
        }


        // ================== Excluir Usuário ==================
        private void Excluir_Usuario(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSelecionado)
            {
                var result = MessageBox.Show(
                    $"Deseja realmente excluir o usuário {usuarioSelecionado.Nome}?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool excluido = controller.ExcluirUsuario(usuarioSelecionado.IdUsuario);
                    if (excluido)
                    {
                        MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CarregarUsuarios();
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
            var home = new Home(usuarioLogado);
            home.Show();
            this.Close();
        }
    }
}
