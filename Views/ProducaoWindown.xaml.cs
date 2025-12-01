using System.Windows; // Necessário para classes de interface (Window, RoutedEventArgs)
using System.Windows.Controls; // Necessário para controles como Button, DataGrid
using WPF_Projeto_BD.Models; // Importa o modelo Usuario

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Tela de gerenciamento da produção
    public partial class ProducaoWindown : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado

        // Construtor da tela, recebe o usuário logado
        public ProducaoWindown(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
        }

        // =========================
        // Botões de controle da produção
        // =========================

        // Evento do botão "Iniciar Produção"
        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar lógica de iniciar produção
        }

        // Evento do botão "Concluir Produção"
        private void btnConcluir_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar lógica de concluir produção
        }

        // Evento do botão "Finalizar Produção"
        private void btnFinalizarProducao_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar lógica de finalizar produção
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new Home(usuarioLogado); // Cria a janela inicial passando o usuário logado
            homeWindow.Show(); // Exibe a janela inicial
            this.Close(); // Fecha a janela atual
        }
    }
}

/*
Resumo técnico:
- ProducaoWindown é a View responsável pelo gerenciamento da produção na empresa.
- Recebe o usuário logado para controle de permissões e rastreamento.
- Possui botões para iniciar, concluir e finalizar processos de produção.
- Todos os eventos atualmente estão vazios e servem como placeholders para futura implementação da lógica de produção via controller.
- Segue o padrão MVC: a View apenas apresenta os controles; toda lógica de negócio deve ser implementada no controller correspondente.
*/
