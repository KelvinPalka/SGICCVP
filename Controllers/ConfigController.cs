using System;
using System.Windows;

using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Controllers
{
    public class ConfigController
    {
        private readonly Usuario usuarioLogado; // Armazena o usuário logado para ser usado nas telas

        public ConfigController(Usuario usuario) // Construtor que recebe o usuário logado
        {
            usuarioLogado = usuario; // Inicializa a variável com o usuário recebido
        }

        // ------------------------
        // Navegação
        // ------------------------

        public void AbrirHome(Window viewAtual) // Abre a tela Home
        {
            try // Tenta executar o bloco
            {
                new Home(usuarioLogado).Show(); // Instancia e mostra a tela Home, passando o usuário logado
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex) // Captura qualquer exceção
            {
                MessageBox.Show("Erro ao abrir a tela Home: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                // Exibe mensagem de erro detalhada
            }
        }

        public void AbrirEmpresaDados(Window viewAtual) // Abre a tela de dados da empresa
        {
            try
            {
                new EmpresaDados(usuarioLogado).Show(); // Mostra a tela EmpresaDados
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir tela da Empresa: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirUsuarioCadastro(Window viewAtual) // Abre a tela de cadastro de usuário
        {
            try
            {
                new UsuarioCadastro(usuarioLogado).Show(); // Mostra a tela UsuarioCadastro
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir cadastro de usuário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirUsuarioLista(Window viewAtual) // Abre a lista de usuários
        {
            try
            {
                new UsuarioLista(usuarioLogado).Show(); // Mostra a tela UsuarioLista
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir lista de usuários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirFuncionarioCadastro(Window viewAtual) // Abre o cadastro de funcionário
        {
            try
            {
                new FuncionarioCadastro(usuarioLogado).Show(); // Mostra a tela FuncionarioCadastro
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir cadastro de funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AbrirFuncionarioLista(Window viewAtual) // Abre a lista de funcionários
        {
            try
            {
                new FuncionarioLista(usuarioLogado).Show(); // Mostra a tela FuncionarioLista
                viewAtual.Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir lista de funcionários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Outros métodos de navegação do sistema (materiais, backup, preferências) implementação futura
    }
}
