using System;
using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Controllers
{
    public class ConfigController
    {
        private readonly Usuario usuarioLogado;

        public ConfigController(Usuario usuario)
        {
            usuarioLogado = usuario;
        }

        // ------------------------
        // Navegação
        // ------------------------
        public void AbrirHome(Window viewAtual)
        {
            try
            {
                new Home(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir a tela Home: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirEmpresaDados(Window viewAtual)
        {
            try
            {
                new EmpresaDados(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir tela da Empresa: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirUsuarioCadastro(Window viewAtual)
        {
            try
            {
                new UsuarioCadastro(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir cadastro de usuário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirUsuarioLista(Window viewAtual)
        {
            try
            {
                new UsuarioLista(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir lista de usuários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirFuncionarioCadastro(Window viewAtual)
        {
            try
            {
                new FuncionarioCadastro(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir cadastro de funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirFuncionarioLista(Window viewAtual)
        {
            try
            {
                new FuncionarioLista(usuarioLogado).Show();
                viewAtual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir lista de funcionários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Outros métodos de navegação do sistema (materiais, backup, preferências) implementação futura
    }
}
