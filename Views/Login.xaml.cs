using System.Windows;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Views
{
    public partial class Login: Window
    {
        private readonly LoginController controller = new LoginController();

        public Login()
        {
            InitializeComponent();
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUsuario.Text;
            string senha = txtSenha.Password;

            var usuario = controller.Autenticar(email, senha);

            if (usuario != null)
            {
                Home home = new Home(usuario);
                Application.Current.MainWindow = home;
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show( "Email ou senha incorretos.");
            }
        }
    }
}
