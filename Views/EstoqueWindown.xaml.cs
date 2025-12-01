using System.Windows; // Necessário para classes de interface (Window, RoutedEventArgs)
using System.Windows.Controls; // Necessário para controles como DataGrid, ComboBox, etc.
using WPF_Projeto_BD.Models; // Importa o modelo Usuario

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Tela principal de gerenciamento de estoque
    public partial class EstoqueWindown : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado

        // Construtor da tela, recebe o usuário logado
        public EstoqueWindown(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
        }

        // ============================================
        // ABA: Matérias-Primas
        // ============================================

        // Evento do botão "Filtrar Matérias-Primas"
        private void btnFiltrarMP_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar filtragem de matérias-primas
        }

        // ============================================
        // ABA: Produtos
        // ============================================

        // Evento do botão "Buscar Produto"
        private void btnBuscarProduto_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar busca de produtos
        }

        // ============================================
        // ABA: Atualizações de Estoque
        // ============================================

        // Evento do botão "Registrar Movimentação"
        private void btnRegistrarMov_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar registro de entrada/saída de estoque
        }

        // ============================================
        // ABA: Relatórios
        // ============================================

        // Evento do botão "Gerar Relatório"
        private void btnGerarRelatorio_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar geração de relatórios de estoque
        }

        // ============================================
        // SELEÇÕES NAS TABELAS (caso precise)
        // ============================================

        // Evento quando seleciona um item na tabela de Matérias-Primas
        private void dgMateriasPrimas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: implementar ação ao selecionar matéria-prima
        }

        // Evento quando seleciona um item na tabela de Produtos
        private void dgProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: implementar ação ao selecionar produto
        }

        // Evento quando seleciona um item na tabela de Movimentações
        private void dgMovimentacoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: implementar ação ao selecionar movimentação de estoque
        }
    }
}

/*
Resumo técnico:
- EstoqueWindown é a View responsável por gerenciar o estoque da empresa.
- Recebe o usuário logado para eventuais permissões e filtragens.
- Possui abas: Matérias-Primas, Produtos, Atualizações de Estoque e Relatórios.
- Todos os eventos atualmente estão vazios, servindo como placeholders para futura implementação da lógica no controller.
- Os DataGrids possuem eventos de seleção, permitindo ações detalhadas quando o usuário interagir com os registros.
- Segue o padrão MVC: a View apenas apresenta os controles; toda lógica de negócio e manipulação de dados deve ser implementada no controller correspondente.
*/
