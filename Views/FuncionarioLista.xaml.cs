using System.Windows;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para FuncionarioLista.xaml
    /// </summary>
    public partial class FuncionarioLista : Window
    {
        private Usuario usuarioLogado;
        private FuncionarioController controller;

        public FuncionarioLista(Usuario usuario)
        {
            InitializeComponent();
            controller = new FuncionarioController();
            usuarioLogado = usuario;

            CarregarFuncionarios();
        }

        // =========================
        // Carregar lista de funcionários
        // =========================
        private void CarregarFuncionarios()
        {
            dgFuncionarios.ItemsSource = controller.ObterTodos(usuarioLogado.IdEmpresa);
        }

        // =========================
        // Botão Adicionar Funcionário
        // =========================
        private void Adicionar_Funcionario(object sender, RoutedEventArgs e)
        {
            var cadastrar = new FuncionarioCadastro(usuarioLogado);
            cadastrar.Show();
            this.Close();
        }

        // =========================
        // Botão Editar Funcionário
        // =========================
        private void Editar_Funcionario(object sender, RoutedEventArgs e)
        {
            if (dgFuncionarios.SelectedItem is Funcionario f)
            {
                var editar = new Home(usuarioLogado);
                editar.Show();
                this.Close();
            }
        }

        // =========================
        // Botão Excluir Funcionário
        // =========================
        private void Excluir_Funcionario(object sender, RoutedEventArgs e)
        {
            if (dgFuncionarios.SelectedItem is Funcionario f)
            {
                var result = MessageBox.Show($"Deseja excluir o funcionário {f.Nome}?", "Confirmação", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    controller.ExcluirFuncionario(f.Id);
                    CarregarFuncionarios();
                }
            }
        }

        // =========================
        // Botão Voltar
        // =========================
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuarioLogado);
            home.Show();
            this.Close();
        }
    }
}
