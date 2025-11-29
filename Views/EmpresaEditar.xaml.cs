using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para EmpresaEditar.xaml
    /// </summary>
    public partial class EmpresaEditar : Window
    {
        private Usuario usuarioLogado;
        private int idEmpresa;
        private Empresa empresaAtual;
        private EmpresaController controller = new EmpresaController();

        public EmpresaEditar(Usuario usuario, int id_empresa)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            idEmpresa = id_empresa;

            // Carrega os dados da empresa ao abrir a tela
            ObterEmpresaPorId();
        }

        private void ObterEmpresaPorId()
        {
            // Chama o Controller para obter a empresa pelo ID
            empresaAtual = controller.ObterEmpresa(idEmpresa);

            if (empresaAtual == null)
            {
                MessageBox.Show("Empresa não encontrada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Preenche os TextBox com os dados atuais da empresa
            txtCNPJ.Text = empresaAtual.CNPJ;
            txtNomeFantasia.Text = empresaAtual.Nome_fantasia;
            txtRazaoSocial.Text = empresaAtual.Razao_social;
            txtEmail.Text = empresaAtual.Email;
            txtTelefone.Text = empresaAtual.Telefone;
            txtEndereco.Text = empresaAtual.Endereco;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            empresaAtual.CNPJ = txtCNPJ.Text;
            empresaAtual.Nome_fantasia = txtNomeFantasia.Text;
            empresaAtual.Razao_social = txtRazaoSocial.Text;
            empresaAtual.Email = txtEmail.Text;
            empresaAtual.Telefone = txtTelefone.Text;
            empresaAtual.Endereco = txtEndereco.Text;

            controller.EditarEmpresa(empresaAtual);
            bool sucesso = controller.EditarEmpresa(empresaAtual);

            if (sucesso)
            {
                MessageBox.Show("Dados da empresa atualizados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Volta para a tela de visualização
                EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
                empresaDadosWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar os dados da empresa.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
            empresaDadosWindow.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
            empresaDadosWindow.Show();
            this.Close();
        }
    }
}
