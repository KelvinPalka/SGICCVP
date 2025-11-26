using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ConnectionTest.Test();                         
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        private void Cadastro(object sender, RoutedEventArgs e)
        {
            var cadastroWindow = new EmpresaCadastro();
            cadastroWindow.Show();
            this.Close();
        }

    }
}