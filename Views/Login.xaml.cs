using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Controllers; // Importa o controller LoginController
using WPF_Projeto_BD.Views; // Importa Views como Home

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Tela de login do sistema
    public partial class Login : Window
    {
        private readonly LoginController controller = new LoginController(); // Controller responsável por autenticação

        // Construtor da tela de login
        public Login()
        {
            InitializeComponent(); // Inicializa os componentes visuais
        }

        // Evento do botão "Entrar"
        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUsuario.Text; // Captura o e-mail digitado
            string senha = txtSenha.Password; // Captura a senha digitada

            // Chama o controller para autenticar usuário
            var usuario = controller.Autenticar(email, senha);

            if (usuario != null) // Se a autenticação foi bem-sucedida
            {
                Home home = new Home(usuario); // Cria a tela principal
                Application.Current.MainWindow = home; // Define como janela principal
                home.Show(); // Exibe a tela Home
                this.Close(); // Fecha a tela de login
            }
            else // Usuário não autenticado
            {
                MessageBox.Show("Email ou senha incorretos."); // Mensagem de erro
            }
        }
    }
}

/*
Resumo técnico:
- Login é a View responsável por autenticar usuários no sistema.
- Segue o padrão MVC: a View captura os dados do formulário e delega a autenticação ao LoginController.
- Se a autenticação for bem-sucedida, abre a tela Home e fecha o Login.
- Caso contrário, exibe mensagem de erro.
- Nenhuma lógica de validação ou acesso direto ao banco ocorre na View; tudo é tratado pelo controller.
*/
