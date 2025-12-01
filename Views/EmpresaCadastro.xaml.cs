using System; // Importa namespaces essenciais do C#
using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Controllers; // Importa o namespace que contém o EmpresaController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela para cadastrar empresa junto com o administrador
    /// </summary>
    public partial class EmpresaCadastro : Window
    {
        private readonly EmpresaController controller; // Controller responsável por gerenciar cadastro de empresas

        // Construtor da tela de cadastro de empresa
        public EmpresaCadastro()
        {
            InitializeComponent(); // Inicializa os componentes visuais
            controller = new EmpresaController(); // Cria o controller (sem usuário logado)
        }

        // Evento do botão "Cadastrar"
        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            // Valida todos os campos antes de prosseguir
            if (!ValidarCampos())
                return;

            try
            {
                // Chama o controller para cadastrar a empresa e o administrador simultaneamente
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

                // Exibe mensagem de sucesso
                MessageBox.Show(
                    "Empresa cadastrada com sucesso!",
                    "Sucesso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                // Após cadastro, abre a tela de login
                var loginWindow = new Login();
                loginWindow.Show();
                this.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                // Exibe mensagem caso ocorra erro inesperado
                MessageBox.Show(
                    "Erro ao cadastrar empresa: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        // Evento do botão "Voltar"
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow(); // Cria a tela inicial
            main.Show(); // Exibe a tela inicial
            this.Close(); // Fecha a tela de cadastro
        }

        // Método que valida todos os campos da tela
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

            return true; // Todos os campos válidos
        }
    }
}

/*
Resumo técnico:
- EmpresaCadastro é a View responsável por cadastrar uma empresa e seu administrador.
- Segue o padrão MVC + DAO: toda lógica de cadastro é delegada ao EmpresaController.
- Possui validação completa dos campos, incluindo CNPJ, e-mails, telefone e senhas.
- Ao concluir cadastro com sucesso, abre a tela de login.
- Possui botão de retorno que abre a tela inicial.
- Nenhuma lógica de persistência de dados está na View; tudo é tratado pelo controller.
*/
