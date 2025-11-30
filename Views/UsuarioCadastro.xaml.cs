using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para UsuarioCadastro.xaml
    /// </summary>
    public partial class UsuarioCadastro : Window
    {
        private Usuario usuarioLogado;
        private UsuarioController usuarioController = new UsuarioController();
        private FuncionarioController funcionarioController = new FuncionarioController();

        public UsuarioCadastro(Usuario usuario)
        {
            InitializeComponent();

            usuarioLogado = usuario;

            // Inicializa visibilidade inicial
            spManual.Visibility = Visibility.Visible;
            spPadrao.Visibility = Visibility.Collapsed;

            // Associa eventos após InitializeComponent
            rbManual.Checked += RbModo_Checked;
            rbPadrao.Checked += RbModo_Checked;

            // Carrega lista de funcionários para modo automático
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios()
        {
            var funcionarios = funcionarioController.ObterTodos(usuarioLogado.IdEmpresa);
            icFuncionariosParaGerar.ItemsSource = funcionarios;
        }

        private void RbModo_Checked(object sender, RoutedEventArgs e)
        {
            if (rbManual.IsChecked == true)
            {
                spManual.Visibility = Visibility.Visible;
                spPadrao.Visibility = Visibility.Collapsed;
            }
            else
            {
                spManual.Visibility = Visibility.Collapsed;
                spPadrao.Visibility = Visibility.Visible;
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new Config(usuarioLogado);
            configWindow.Show();
            this.Close();
        }

        // ==================== Cadastro Manual ====================
        private void BtnSalvarManual_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNomeUsuario.Text.Trim();
            string email = txtEmailUsuario.Text.Trim();
            string senha = txtSenhaUsuario.Password;
            string tipo = ((ComboBoxItem)cmbTipoUsuario.SelectedItem).Content.ToString();
            int idEmpresa = usuarioLogado.IdEmpresa;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string resultado = usuarioController.CadastrarUsuario(nome, email, senha, tipo, idEmpresa);

            if (resultado == "ok")
            {
                MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + resultado, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==================== Cadastro Automático ====================
        private void BtnGerarPadrao_Click(object sender, RoutedEventArgs e)
        {
            var checkboxes = icFuncionariosParaGerar.Items.OfType<Funcionario>()
                .Select(f => new
                {
                    Funcionario = f,
                    IsChecked = GetCheckBoxForFuncionario(f)?.IsChecked == true
                }).Where(x => x.IsChecked).ToList();

            if (!checkboxes.Any())
            {
                MessageBox.Show("Selecione ao menos um funcionário.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var item in checkboxes)
            {
                string nome = item.Funcionario.Nome;
                string email = item.Funcionario.Email;
                string senha = item.Funcionario.CPF; // Senha padrão = CPF
                string tipo = "user";
                int idEmpresa = usuarioLogado.IdEmpresa;

                usuarioController.CadastrarUsuario(nome, email, senha, tipo, idEmpresa);
            }

            MessageBox.Show("Usuários gerados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            var configWindow = new Config(usuarioLogado);
            configWindow.Show();
            this.Close();
        }

        private CheckBox GetCheckBoxForFuncionario(Funcionario funcionario)
        {
            foreach (var item in icFuncionariosParaGerar.Items)
            {
                var container = icFuncionariosParaGerar.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    var cb = FindVisualChild<CheckBox>(container);
                    if (cb != null && (int)cb.Tag == funcionario.Id)
                        return cb;
                }
            }
            return null;
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T t)
                    return t;

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }
    }
}
