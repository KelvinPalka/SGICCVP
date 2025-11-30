using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Views
{
    public partial class FuncionarioCadastro : Window
    {
        private Usuario usuarioLogado;
        private FuncionarioController controller = new FuncionarioController();
        private FuncionarioDAO funcionarioDao = new FuncionarioDAO();
        private Funcionario funcionarioEmEdicao;

        public FuncionarioCadastro(Usuario usuario, Funcionario funcionarioParaEditar = null)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            funcionarioEmEdicao = funcionarioParaEditar;

            if (funcionarioEmEdicao != null)
            {
                PreencherCamposEdicao();
                btnSalvar.Content = "Atualizar Funcionário";
            }
        }

        private void PreencherCamposEdicao()
        {
            txtNomeFuncionario.Text = funcionarioEmEdicao.Nome;
            txtCPF.Text = funcionarioEmEdicao.CPF;
            txtCargo.Text = funcionarioEmEdicao.Cargo;
            txtTelefone.Text = funcionarioEmEdicao.Telefone;
            txtEmailFuncionario.Text = funcionarioEmEdicao.Email;

            cmbDepartamento.SelectedItem = cmbDepartamento.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(c => c.Content.ToString() == funcionarioEmEdicao.Departamento);
        }

        // ==================== Validação de campos ====================
        private string ValidarCampos(string nome, string cpf, string cargo, string telefone, string email, string departamento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return "O nome é obrigatório.";

            if (string.IsNullOrWhiteSpace(cpf))
                return "O CPF é obrigatório.";

            if (string.IsNullOrWhiteSpace(cargo))
                return "O cargo é obrigatório.";

            if (string.IsNullOrWhiteSpace(telefone))
                return "O telefone é obrigatório.";

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return "O e-mail é inválido.";

            if (string.IsNullOrWhiteSpace(departamento))
                return "O departamento é obrigatório.";

            // Verificar duplicidade de CPF ou e-mail
            var todos = controller.ObterTodos(usuarioLogado.IdEmpresa);
            foreach (var f in todos)
            {
                if (funcionarioEmEdicao != null && f.Id == funcionarioEmEdicao.Id) continue;

                if (f.CPF == cpf)
                    return "Este CPF já está cadastrado para outro funcionário.";

                if (f.Email == email)
                    return "Este e-mail já está cadastrado para outro funcionário.";
            }

            return "ok";
        }

        // ==================== Botão Salvar ====================
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNomeFuncionario.Text.Trim();
            string cpf = txtCPF.Text.Trim();
            string cargo = txtCargo.Text.Trim();
            string telefone = txtTelefone.Text.Trim();
            string email = txtEmailFuncionario.Text.Trim();
            string departamento = ((ComboBoxItem)cmbDepartamento.SelectedItem)?.Content?.ToString() ?? "";

            string resultadoValidacao = ValidarCampos(nome, cpf, cargo, telefone, email, departamento);
            if (resultadoValidacao != "ok")
            {
                MessageBox.Show(resultadoValidacao, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (funcionarioEmEdicao != null)
                {
                    // Atualização
                    funcionarioEmEdicao.Nome = nome;
                    funcionarioEmEdicao.CPF = cpf;
                    funcionarioEmEdicao.Cargo = cargo;
                    funcionarioEmEdicao.Telefone = telefone;
                    funcionarioEmEdicao.Email = email;
                    funcionarioEmEdicao.Departamento = departamento;

                    string resultado = controller.AtualizarFuncionario(funcionarioEmEdicao);
                    if (resultado == "ok")
                        MessageBox.Show("Funcionário atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Erro ao atualizar funcionário!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Cadastro
                    string resultado = controller.CadastrarFuncionario(nome, cpf, cargo, telefone, email, usuarioLogado.IdEmpresa, departamento);
                    if (resultado == "ok")
                        MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Erro ao cadastrar funcionário!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Abrir lista e fechar cadastro
                var lista = new FuncionarioLista(usuarioLogado);
                lista.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==================== Cancelar / Voltar ====================
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var configwindow = new Config(usuarioLogado);
            configwindow.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var configwindow = new Config(usuarioLogado);
            configwindow.Show();
            this.Close();
        }
    }
}
