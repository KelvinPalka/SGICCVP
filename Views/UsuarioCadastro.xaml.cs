using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;

namespace WPF_Projeto_BD.Views
{
    public partial class UsuarioCadastro : Window
    {
        private Usuario usuarioLogado;
        private UsuarioController usuarioController = new UsuarioController();
        private FuncionarioController funcionarioController = new FuncionarioController();
        private Usuario usuarioEmEdicao;

        public UsuarioCadastro(Usuario usuarioLogado, Usuario usuarioParaEditar = null)
        {
            InitializeComponent();
            this.usuarioLogado = usuarioLogado;
            this.usuarioEmEdicao = usuarioParaEditar;

            // Modo inicial
            spManual.Visibility = Visibility.Visible;
            spPadrao.Visibility = Visibility.Collapsed;

            // Eventos
            rbManual.Checked += RbModo_Checked;
            rbPadrao.Checked += RbModo_Checked;

            CarregarFuncionarios();

            // Preencher dados se estiver editando
            if (usuarioEmEdicao != null)
            {
                PreencherCamposEdicao();
            }
        }

        private void PreencherCamposEdicao()
        {
            rbManual.IsChecked = true;

            txtNomeUsuario.Text = usuarioEmEdicao.Nome;
            txtEmailUsuario.Text = usuarioEmEdicao.Email;
            txtSenhaUsuario.Password = ""; // por segurança não preenche

            cmbTipoUsuario.SelectedItem =
                cmbTipoUsuario.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == usuarioEmEdicao.TipoUsuario);

            btnSalvarManual.Content = "Atualizar Usuário";
        }

        private void CarregarFuncionarios()
        {
            var funcionarios = funcionarioController.ObterTodos(usuarioLogado.IdEmpresa);
            icFuncionariosParaGerar.ItemsSource = funcionarios;
        }

        private void RbModo_Checked(object sender, RoutedEventArgs e)
        {
            spManual.Visibility = rbManual.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            spPadrao.Visibility = rbPadrao.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
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
            string tipoSelecionado = ((ComboBoxItem)cmbTipoUsuario.SelectedItem)?.Content?.ToString() ?? "";

            try
            {
                // Valida campos obrigatórios
                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Nome e e-mail são obrigatórios.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Valida formato de e-mail
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("E-mail inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validação obrigatória do tipo de conta
                if (string.IsNullOrWhiteSpace(tipoSelecionado) || (tipoSelecionado != "admin" && tipoSelecionado != "user"))
                {
                    MessageBox.Show("Selecione o tipo de conta ('admin' ou 'user').", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string resultado;

                if (usuarioEmEdicao == null)
                {
                    // ================= CADASTRO =================
                    if (!SenhaValida(senha, false))
                    {
                        MessageBox.Show("Senha inválida. Mínimo 6 caracteres, com letras e números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    resultado = usuarioController.CadastrarUsuario(nome, email, senha, tipoSelecionado, usuarioLogado.IdEmpresa);

                    if (resultado == "ok")
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar usuário: " + resultado, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    // ================= EDIÇÃO =================
                    usuarioEmEdicao.Nome = nome;
                    usuarioEmEdicao.Email = email;
                    usuarioEmEdicao.TipoUsuario = tipoSelecionado;

                    // ================= VALIDAÇÃO DE SENHA =================
                    if (string.IsNullOrWhiteSpace(usuarioEmEdicao.SenhaHash) && string.IsNullOrWhiteSpace(senha))
                    {
                        // Caso não exista senha anterior e o campo está vazio, não permite
                        MessageBox.Show("A senha não pode ficar vazia.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(senha) && !SenhaValida(senha, false))
                    {
                        MessageBox.Show("A senha deve ter pelo menos 6 caracteres, com letras e números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Atualiza hash apenas se nova senha for preenchida
                    if (!string.IsNullOrWhiteSpace(senha))
                    {
                        usuarioEmEdicao.SenhaHash = senha; // o controller irá gerar hash
                    }

                    resultado = usuarioController.AtualizarUsuario(usuarioEmEdicao);

                    if (resultado == "ok")
                    {
                        MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        var usuarioLista = new UsuarioLista(usuarioLogado);
                        usuarioLista.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar usuário: " + resultado, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

                string resultado = usuarioController.CadastrarUsuario(nome, email, senha, tipo, idEmpresa, true);

                if (resultado != "ok")
                {
                    MessageBox.Show($"Erro ao gerar usuário {nome}: {resultado}", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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

        // Valida se a senha contém pelo menos 6 caracteres, com letra e número
        private bool SenhaValida(string senha, bool permitirApenasNumeros)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                return false;

            if (permitirApenasNumeros)
                return true; // permite CPF numérico

            bool temLetra = senha.Any(char.IsLetter);
            bool temNumero = senha.Any(char.IsDigit);

            return temLetra && temNumero;
        }
    }
}
