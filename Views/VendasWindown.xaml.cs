using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class VendasWindown : Window
    {
        private Usuario usuarioLogado;
        public VendasWindown(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
        }

        // ========================
        // PLACEHOLDER DO TEXTBOX
        // ========================

        private void TextBoxPlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        private void TextBoxPlaceholder_LostFocus(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ========================
        // BOTÃO: APLICAR FILTROS
        // ========================

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            // evento vazio
        }

        // ========================
        // SELEÇÃO DA TABELA DE VENDAS
        // ========================

        private void dgVendas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // evento vazio
        }

        private void txtClienteFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
