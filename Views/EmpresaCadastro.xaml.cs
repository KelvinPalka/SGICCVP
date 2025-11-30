using System;
using System.Windows;
using WPF_Projeto_BD.Controllers;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Tela para cadastrar empresa + administrador
    /// </summary>
    public partial class EmpresaCadastro : Window
    {
        private readonly EmpresaController controller;

        public EmpresaCadastro()
        {
            InitializeComponent();
            controller = new EmpresaController(); // Controller sem usuário logado
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
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

                MessageBox.Show("Empresa cadastrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Após cadastro, abrir a tela de login
                var loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar empresa: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow(); // voltar para tela inicial
            main.Show();
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCNPJ.Text) || txtCNPJ.Text.Length < 14)
            {
                MessageBox.Show("CNPJ inválido ou vazio.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNomeFantasia.Text))
            {
                MessageBox.Show("Nome Fantasia é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRazaoSocial.Text))
            {
                MessageBox.Show("Razão Social é obrigatória.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!txtEmailEmpresa.Text.Contains("@") || !txtEmailEmpresa.Text.Contains("."))
            {
                MessageBox.Show("E-mail da empresa inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtTelefone.Text.Length < 10)
            {
                MessageBox.Show("Telefone inválido. Use DDD + número.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("Endereço é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNomeADM.Text))
            {
                MessageBox.Show("Nome do administrador é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!txtEmailADM.Text.Contains("@") || !txtEmailADM.Text.Contains("."))
            {
                MessageBox.Show("E-mail do administrador inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtSenhaADM.Password.Length < 6)
            {
                MessageBox.Show("A senha deve ter pelo menos 6 caracteres.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtSenhaADM.Password != txtConfirmSenhaADM.Password)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
