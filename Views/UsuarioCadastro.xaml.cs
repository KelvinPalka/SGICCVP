using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Wpf_Projeto_BD.Models; // Model Usuario e Funcionario
using WPF_Projeto_BD.Controllers; // Controllers UsuarioController e FuncionarioController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela para cadastro e edição de usuários do sistema
    /// </summary>
    public partial class UsuarioCadastro : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado
        private UsuarioController usuarioController = new UsuarioController(); // Controller de usuários
        private FuncionarioController funcionarioController = new FuncionarioController(); // Controller de funcionários
        private Usuario usuarioEmEdicao; // Se não nulo, indica edição de usuário existente

        // Construtor da tela, recebe usuário logado e opcionalmente usuário para edição
        public UsuarioCadastro(Usuario usuarioLogado, Usuario usuarioParaEditar = null)
        {
            InitializeComponent();
            this.usuarioLogado = usuarioLogado;
            this.usuarioEmEdicao = usuarioParaEditar;

            // Inicializa modo manual como padrão
            spManual.Visibility = Visibility.Visible;
            spPadrao.Visibility = Visibility.Collapsed;

            // Eventos de alternância entre modos
            rbManual.Checked += RbModo_Checked;
            rbPadrao.Checked += RbModo_Checked;

            CarregarFuncionarios(); // Preenche lista de funcionários

            // Preenche campos se estiver editando usuário existente
            if (usuarioEmEdicao != null)
                PreencherCamposEdicao();
        }

        // Preenche os campos da tela para edição
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

        // Carrega funcionários para gerar usuários automaticamente
        private void CarregarFuncionarios()
        {
            var funcionarios = funcionarioController.ObterTodos(usuarioLogado.IdEmpresa);
            icFuncionariosParaGerar.ItemsSource = funcionarios;
        }

        // Alterna visibilidade entre modo Manual e Padrão
        private void RbModo_Checked(object sender, RoutedEventArgs e)
        {
            spManual.Visibility = rbManual.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            spPadrao.Visibility = rbPadrao.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        // Botão Voltar para tela Configurações
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
            string novaSenha = txtSenhaUsuario.Password;
            string tipoSelecionado = ((ComboBoxItem)cmbTipoUsuario.SelectedItem)?.Content?.ToString() ?? "";

            try
            {
                // ================= VALIDAÇÃO DE CAMPOS =================
                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Nome e e-mail são obrigatórios.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("E-mail inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tipoSelecionado) || (tipoSelecionado != "admin" && tipoSelecionado != "user"))
                {
                    MessageBox.Show("Selecione o tipo de conta ('admin' ou 'user').", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string resultado;

                if (usuarioEmEdicao == null)
                {
                    // ================= CADASTRO =================
                    if (!SenhaValida(novaSenha, false))
                    {
                        MessageBox.Show("Senha inválida. Mínimo 6 caracteres, com letras e números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    resultado = usuarioController.CadastrarUsuario(nome, email, novaSenha, tipoSelecionado, usuarioLogado.IdEmpresa);

                    if (resultado == "ok")
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        var configWindow = new Config(usuarioLogado);
                        configWindow.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Erro ao cadastrar usuário: " + resultado, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    // ================= EDIÇÃO =================
                    usuarioEmEdicao.Nome = nome;
                    usuarioEmEdicao.Email = email;
                    usuarioEmEdicao.TipoUsuario = tipoSelecionado;

                    if (string.IsNullOrWhiteSpace(novaSenha))
                    {
                        MessageBox.Show("A senha não pode ficar vazia na edição.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!SenhaValida(novaSenha, false))
                    {
                        MessageBox.Show("A senha deve ter pelo menos 6 caracteres, com letras e números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    resultado = usuarioController.AtualizarUsuario(usuarioEmEdicao, novaSenha);

                    if (resultado == "ok")
                    {
                        MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        var usuarioLista = new UsuarioLista(usuarioLogado);
                        usuarioLista.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Erro ao atualizar usuário: " + resultado, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            // icFuncionariosParaGerar é um controle da interface (ItemsControl/ListBox/ListView)
            // Ele exibe todos os funcionários que podem ter usuários gerados automaticamente
            // Cada item em icFuncionariosParaGerar.Items é um objeto, mas nem todos necessariamente são do tipo Funcionario
            // Exemplo de Items: [ Funcionario {Id=1, Nome="João"}, Funcionario {Id=2, Nome="Maria"}, Funcionario {Id=3, Nome="Pedro"} ]

            // Pega todos os funcionários que estão na lista 'icFuncionariosParaGerar',
            // verifica se o CheckBox de cada funcionário está marcado, e cria uma lista temporária.
            var checkboxes = icFuncionariosParaGerar.Items
                .OfType<Funcionario>()  // Filtra apenas os objetos que são do tipo Funcionario
                                        // Ex.: [João, Maria, Pedro]

                .Select(f => new          // Para cada funcionário f, cria um objeto com:
                {
                    Funcionario = f,     // Armazena o próprio funcionário
                                         // IsChecked será true se o CheckBox correspondente estiver marcado
                                         // GetCheckBoxForFuncionario(f) retorna o CheckBox visual do funcionário
                                         // ?.IsChecked verifica se o CheckBox existe e se está marcado
                                         // == true garante que seja booleano verdadeiro apenas se estiver marcado
                    IsChecked = GetCheckBoxForFuncionario(f)?.IsChecked == true
                })

                // Filtra apenas os funcionários que estão realmente marcados
                // Exemplo: [{João,true}, {Maria,false}, {Pedro,true}] -> [{João,true}, {Pedro,true}]
                .Where(x => x.IsChecked)

                // Converte o resultado final em uma lista concreta
                // Agora podemos percorrer a lista no foreach para criar os usuários
                .ToList();

            // Verifica se o usuário não selecionou nenhum funcionário
            // Se a lista estiver vazia, mostra aviso e encerra o método
            if (!checkboxes.Any())
            {
                // Mostra caixa de diálogo alertando que pelo menos um funcionário precisa ser selecionado
                MessageBox.Show("Selecione ao menos um funcionário.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Sai do método sem criar nenhum usuário
            }

            // Se chegou aqui, significa que há pelo menos um funcionário selecionado
            // Percorre cada funcionário selecionado para gerar o usuário
            foreach (var item in checkboxes)
            {
                // Extrai os dados do funcionário que serão usados para criar o usuário
                string nome = item.Funcionario.Nome;       // Nome do funcionário
                string email = item.Funcionario.Email;     // Email do funcionário
                string senha = item.Funcionario.CPF;       // Senha padrão = CPF
                string tipo = "user";                       // Tipo de usuário padrão
                int idEmpresa = usuarioLogado.IdEmpresa;   // Empresa do usuário logado

                // Chama o controller para cadastrar o usuário
                // O último parâmetro "true" indica que é um cadastro automático
                string resultado = usuarioController.CadastrarUsuario(nome, email, senha, tipo, idEmpresa, true);

                // Verifica se houve algum erro no cadastro
                // Se o resultado não for "ok", mostra mensagem de erro informando o nome do funcionário e o problema
                if (resultado != "ok")
                {
                    MessageBox.Show($"Erro ao gerar usuário {nome}: {resultado}", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // Se todos os usuários foram gerados (ou pelo menos não houve erro crítico),
            // mostra mensagem de sucesso
            MessageBox.Show("Usuários gerados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            // Abre a janela de configuração novamente, passando o usuário logado
            var configWindow = new Config(usuarioLogado);
            configWindow.Show(); // Mostra a nova janela
            this.Close();        // Fecha a janela atual para não duplicar a interface
        }

        // ==================== Métodos auxiliares ====================

        // Procura o CheckBox correspondente a um funcionário específico
        // Exemplo: se o funcionário tem Id=5, procura o CheckBox que tem Tag=5
        private CheckBox GetCheckBoxForFuncionario(Funcionario funcionario)
        {
            // Percorre todos os itens da lista de funcionários
            foreach (var item in icFuncionariosParaGerar.Items)
            {
                // Para cada item, obtém o container visual (cada item da lista tem seu próprio container)
                var container = icFuncionariosParaGerar.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    // Procura dentro do container o CheckBox visualmente
                    var cb = FindVisualChild<CheckBox>(container);

                    // Se encontrou o CheckBox e o Tag dele corresponde ao Id do funcionário,
                    // então este CheckBox pertence ao funcionário
                    if (cb != null && (int)cb.Tag == funcionario.Id)
                        return cb; // Retorna o CheckBox
                }
            }

            // Se não encontrar o CheckBox, retorna null
            return null;
        }

        // Procura recursivamente um filho visual de tipo T dentro de um objeto visual
        // Ex.: você passa um container e quer encontrar um CheckBox dentro dele
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            // Percorre todos os filhos do objeto visual
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);

                // Se o filho for do tipo procurado (ex.: CheckBox), retorna imediatamente
                if (child != null && child is T t)
                    return t;

                // Senão, procura recursivamente dentro dos filhos deste filho
                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild; // Retorna se encontrou
            }

            // Retorna null se não encontrou nenhum filho do tipo T
            return null;
        }

        // Valida se a senha é válida
        // Critérios:
        // 1) Mínimo de 6 caracteres
        // 2) Se não permitir apenas números, deve ter pelo menos uma letra e um número
        private bool SenhaValida(string senha, bool permitirApenasNumeros)
        {
            // Se a senha for nula, vazia ou menor que 6 caracteres, retorna false
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                return false;

            // Se permitido apenas números (como CPF), retorna true
            if (permitirApenasNumeros)
                return true;

            // Verifica se a senha contém pelo menos uma letra
            bool temLetra = senha.Any(char.IsLetter);
            // Verifica se a senha contém pelo menos um número
            bool temNumero = senha.Any(char.IsDigit);

            // Retorna true apenas se tiver pelo menos uma letra e um número
            return temLetra && temNumero;
        }

    }
}

/*
Resumo técnico:
- UsuarioCadastro é a View responsável por cadastrar e editar usuários do sistema.
- Possui dois modos: Manual e Padrão (geração automática de usuários a partir de funcionários).
- Segue padrão MVC + DAO: toda lógica de cadastro, atualização e validação é delegada ao UsuarioController.
- Valida campos obrigatórios, e-mail e senha (mínimo 6 caracteres com letras e números).
- Cadastro padrão gera usuários para funcionários selecionados com senha inicial igual ao CPF.
- Métodos auxiliares permitem localizar CheckBoxes associados a funcionários.
- Navegação de telas: Voltar retorna à Config, após cadastro abre Config ou UsuarioLista.
- Nenhuma lógica de persistência direta ocorre na View; tudo é gerenciado pelo controller.
*/
