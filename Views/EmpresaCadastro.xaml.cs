using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para EmpresaCadastro.xaml
    /// </summary>
    public partial class EmpresaCadastro : Window
    {
        public EmpresaCadastro()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        { 
            var controller = new Controllers.EmpresaController();
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

            MessageBox.Show("Empresa e administrador cadastrados com sucesso!");
            
        }
    }
}
