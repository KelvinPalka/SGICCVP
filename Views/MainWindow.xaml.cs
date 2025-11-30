using System.Windows;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Views
{
	/// <summary>
	/// Interação lógica para MainWindow.xaml
	/// Tela inicial do sistema, sem usuário logado.
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Login(object sender, RoutedEventArgs e)
		{
			var loginWindow = new Login(); // tela de login
			loginWindow.Show();
			this.Close();
		}

		private void Cadastro(object sender, RoutedEventArgs e)
		{
			var cadastroWindow = new EmpresaCadastro(); // tela de cadastro de empresa/admin
			cadastroWindow.Show();
			this.Close();
		}
	}
}
