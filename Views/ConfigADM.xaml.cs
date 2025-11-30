using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Views;

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
            var usuarioCadastrar = new UsuarioCadastro(usuarioLogado);
            usuarioCadastrar.Show();
            this.Close();
        }

        private void BtnUsuarioLista_Click(object sender, RoutedEventArgs e)
        {
            var usuarioLista = new UsuarioLista(usuarioLogado);
            usuarioLista.Show();
            this.Close();
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

        private void BtnFuncionarioLista_Click(object sender, RoutedEventArgs e)
        {
            var lista = new FuncionarioLista(usuarioLogado);
            lista.Show();
            this.Close();
        }

        private void BtnFuncionarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var FuncionarioCadastrar = new FuncionarioCadastro(usuarioLogado);
            FuncionarioCadastrar.Show();
            this.Close();
        }
    }
}
