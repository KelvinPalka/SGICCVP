using System.Windows;
using System.Windows.Controls;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class EstoqueWindown : Window
    {
        private Usuario usuarioLogado;
        public EstoqueWindown(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
        }

        // ============================================
        // ABA: Matérias-Primas
        // ============================================

        private void btnFiltrarMP_Click(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ============================================
        // ABA: Produtos
        // ============================================

        private void btnBuscarProduto_Click(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ============================================
        // ABA: Atualizações de Estoque
        // ============================================

        private void btnRegistrarMov_Click(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ============================================
        // ABA: Relatórios
        // ============================================

        private void btnGerarRelatorio_Click(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ============================================
        // SELEÇÕES NAS TABELAS (caso precise)
        // ============================================

        private void dgMateriasPrimas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // evento vazio
        }

        private void dgProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // evento vazio
        }

        private void dgMovimentacoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // evento vazio
        }
    }
}
