using System.Windows;
using WPF_Projeto_BD.Controllers;

namespace WPF_Projeto_BD.Views
{
    public partial class EmpresaCadastro : Window
    {
        private EmpresaController controller = new EmpresaController();

        public EmpresaCadastro()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            controller.CadastrarEmpresaComAdministrador(
                txtCNPJ.Text,
                txtNomeFantasia.Text,
                txtEmailEmpresa.Text,
                txtTelefone.Text,
                txtRazaoSocial.Text,
                txtEndereco.Text,
                txtNomeADM.Text,
                txtEmailADM.Text,
                txtSenhaADM.Password,
                txtConfirmSenhaADM.Password
            );
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

    }
}
