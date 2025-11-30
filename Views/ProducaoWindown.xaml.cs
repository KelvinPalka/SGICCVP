using System.Windows;
using System.Windows.Controls;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class ProducaoWindown : Window
    {
        private Usuario usuarioLogado;

        public ProducaoWindown(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
        }


        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar
        }


        private void btnConcluir_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar
        }


        private void btnFinalizarProducao_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar
        }
    }
}