using System.Windows; // Necessário para classes de interface (Window, RoutedEventArgs)
using WPF_Projeto_BD.Views; // Importa Views como Login e EmpresaCadastro

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
	/// <summary>
	/// Tela inicial do sistema (MainWindow), exibida sem usuário logado.
	/// </summary>
	public partial class MainWindow : Window
	{
		// Construtor da tela inicial
		public MainWindow()
		{
			InitializeComponent(); // Inicializa os componentes visuais
		}

		// Evento do botão "Login"
		private void Login(object sender, RoutedEventArgs e)
		{
			var loginWindow = new Login(); // Cria a tela de login
			loginWindow.Show(); // Exibe a tela de login
			this.Close(); // Fecha a tela inicial
		}

		// Evento do botão "Cadastro"
		private void Cadastro(object sender, RoutedEventArgs e)
		{
			var cadastroWindow = new EmpresaCadastro(); // Cria a tela de cadastro de empresa/administrador
			cadastroWindow.Show(); // Exibe a tela de cadastro
			this.Close(); // Fecha a tela inicial
		}
	}
}

/*
Resumo técnico:
- MainWindow é a tela inicial do sistema, exibida antes de qualquer usuário estar logado.
- Possui botões para Login e Cadastro de empresa/administrador.
- Segue o padrão MVC: a View apenas abre as telas correspondentes, sem lógica de negócio ou acesso a dados.
- Cada botão cria a janela correspondente e fecha a MainWindow para manter apenas uma tela aberta.
*/
