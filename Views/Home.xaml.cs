using System;
using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Controllers; // Importa controllers como ClienteController
using WPF_Projeto_BD.Models; // Importa o modelo Usuario

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela principal do sistema (Home)
    /// </summary>
    public partial class Home : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado

        // Construtor da tela, recebe o usuário logado
        public Home(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
            VerificarPermissao(); // Ajusta visibilidade de botões conforme tipo de usuário
        }

        // Verifica permissões e ajusta visibilidade dos controles
        private void VerificarPermissao()
        {
            if (usuarioLogado.TipoUsuario == "admin")
            {
                btnConfig.Visibility = Visibility.Visible; // Admin vê o botão de Configurações
            }
            else
            {
                btnConfig.Visibility = Visibility.Collapsed; // Usuário comum não vê
            }
        }

        // =========================
        // Botões de navegação
        // =========================

        private void BtnEstoque_Click(object sender, RoutedEventArgs e)
        {
            var estoqueWindow = new EstoqueWindown(usuarioLogado);
            estoqueWindow.Show();
            this.Close(); // Fecha a tela Home
        }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            var pedidosWindow = new PedidosLista(usuarioLogado);
            pedidosWindow.Show();
            this.Close();
        }

        private void BtnProducao_Click(object sender, RoutedEventArgs e)
        {
            var producao = new ProducaoWindown(usuarioLogado);
            producao.Show();
            this.Close();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            var controller = new ClienteController(usuarioLogado);
            var ClientesWindow = new ClienteLista(controller);
            ClientesWindow.Show();
            this.Close();
        }

        private void BtnVendas_Click(object sender, RoutedEventArgs e)
        {
            var vendas = new VendasWindown(usuarioLogado);
            vendas.Show();
            this.Close();
        }

        // Botão Fechar/Sair do sistema
        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Tem certeza que deseja sair?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }

        // Botão de Configurações (apenas visível para administradores)
        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new Config(usuarioLogado);
            configWindow.Show();
            this.Close();
        }
    }
}

/*
Resumo técnico:
- Home é a View principal do sistema, servindo como painel de navegação.
- Recebe o usuário logado e ajusta visibilidade de botões conforme permissões (admin vs comum).
- Possui botões para acessar Estoque, Pedidos, Produção, Clientes, Vendas e Configurações.
- Botão Fechar solicita confirmação antes de sair e volta para Login.
- Segue o padrão MVC: a View apenas gerencia a navegação; lógica de negócio de cada módulo é delegada aos controllers correspondentes.
*/
