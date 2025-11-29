using System.Windows;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class Config : Window
    {
        private Usuario usuarioLogado;

        public Config(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
        }

        // ------------------------
        // BOTÃO VOLTAR
        // ------------------------
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var home = new Home(usuarioLogado);
            home.Show();
            this.Close();
        }

        // ------------------------
        // EMPRESA
        // ------------------------
        private void BtnDadosEmpresa_Click(object sender, RoutedEventArgs e)
        {
            var empresa = new EmpresaDados(usuarioLogado);
            empresa.Show();
            this.Close();
        }

        private void BtnMateriais_Click(object sender, RoutedEventArgs e)
        {
            // abrir tela de materiais (lista)
        }

        // ------------------------
        // USUÁRIOS E ACESSOS
        // ------------------------
        private void BtnUsuarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            // abrir tela de cadastro de usuário
        }

        private void BtnUsuarioLista_Click(object sender, RoutedEventArgs e)
        {
            // abrir tela lista de usuários
        }

        private void BtnRedefinirSenha_Click(object sender, RoutedEventArgs e)
        {
            // redefinir senha própria
        }

        // ------------------------
        // SISTEMA
        // ------------------------
        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            // backup do sistema
        }

        private void BtnLimparRegistros_Click(object sender, RoutedEventArgs e)
        {
            // limpar logs, pedidos cancelados etc.
        }

        private void BtnPreferencias_Click(object sender, RoutedEventArgs e)
        {
            // preferências do sistema
        }
    }
}
