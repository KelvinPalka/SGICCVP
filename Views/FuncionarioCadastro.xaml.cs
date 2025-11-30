using System.Windows;
using System.Windows.Controls;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para FuncionarioCadastro.xaml
    /// </summary>
    public partial class FuncionarioCadastro : Window
    {
        private Usuario usuarioLogado;
        private FuncionarioController controller = new FuncionarioController();

        public FuncionarioCadastro(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
        }

        // Evento do botão Salvar
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            int idEmpresa = usuarioLogado.IdEmpresa;

            // Pega o departamento selecionado
            string departamento = ((ComboBoxItem)cmbDepartamento.SelectedItem)?.Content?.ToString() ?? "";

            // Cadastrar funcionário
            string resultado = controller.CadastrarFuncionario(
                txtNomeFuncionario.Text,
                txtCPF.Text,
                txtCargo.Text,
                txtTelefone.Text,
                txtEmailFuncionario.Text,
                idEmpresa,
                departamento
            );

            if (resultado == "ok")
            {
                MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Abre a lista de funcionários
                var lista = new FuncionarioLista(usuarioLogado);
                lista.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar funcionário!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Evento do botão Cancelar
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var configwindow = new Config(usuarioLogado);
            configwindow.Show();
            this.Close();
        }

        // Evento do botão Voltar
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var configwindow = new Config(usuarioLogado);
            configwindow.Show();
            this.Close();
        }
    }
}
