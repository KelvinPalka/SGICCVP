using System.Windows;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class Config : Window
    {
        private readonly ConfigController controller;

        public Config(Usuario usuario)
        {
            InitializeComponent();
            controller = new ConfigController(usuario);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirHome(this);
        }

        private void BtnDadosEmpresa_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirEmpresaDados(this);
        }

        private void BtnUsuarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirUsuarioCadastro(this);
        }

        private void BtnUsuarioLista_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirUsuarioLista(this);
        }

        private void BtnFuncionarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirFuncionarioCadastro(this);
        }

        private void BtnFuncionarioLista_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirFuncionarioLista(this);
        }

        private void BtnMateriais_Click(object sender, RoutedEventArgs e)
        {

        }

        // Botões ainda não implementados (materiais, backup, preferências, redefinir senha) podem chamar métodos do controller
    }
}
