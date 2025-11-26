using System.Windows;
using System.Windows.Controls;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using System.Text.RegularExpressions;
using MySqlX.XDevAPI;
using WPF_Projeto_BD.Utils;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para ClienteCadastro.xaml
    /// </summary>
    public partial class ClienteCadastro : Window
    {
        private Cliente clienteEditando = null;

        // CONSTRUTOR PARA CADASTRO NOVO
        public ClienteCadastro()
        {
            InitializeComponent();
        }

        // CONSTRUTOR PARA EDIÇÃO
        public ClienteCadastro(Cliente cliente)
        {
            InitializeComponent();

            clienteEditando = cliente;

            // Preencher campos da tela
            txtNome.Text = cliente.Nome;
            txtCPF_CNPJ.Text = cliente.CPF_CNPJ;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
        }

        private bool ValidarCampos()
        {
            // Nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome não pode estar vazio.");
                return false;
            }

            // CPF/CNPJ
            if (string.IsNullOrWhiteSpace(txtCPF_CNPJ.Text))
            {
                MessageBox.Show("CPF/CNPJ não pode estar vazio.");
                return false;
            }

            if (txtCPF_CNPJ.Text.Length < 11) 
            {
                MessageBox.Show("CPF/CNPJ inválido.");
                return false;
            }

            // Endereço
            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("O endereço não pode estar vazio.");
                return false;
            }

            // Telefone
            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("O telefone não pode estar vazio.");
                return false;
            }

            if (txtTelefone.Text.Length < 8)
            {
                MessageBox.Show("Telefone inválido.");
                return false;
            }

            // Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("O email não pode estar vazio.");
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email inválido.");
                return false;
            }

            // Tudo certo
            return true;
        }


        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
                return;

            ClienteController controller = new ClienteController();

            // Se for um novo cliente
            if (clienteEditando == null)
            {
                string cpfLimpo = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", "");
                string telefoneLimpo = Regex.Replace(txtTelefone.Text, "[^0-9]", "");

                Cliente c = new Cliente
                {
                    Nome = txtNome.Text.Trim(),
                    CPF_CNPJ = cpfLimpo,
                    Endereco = txtEndereco.Text.Trim(),
                    Telefone = telefoneLimpo,
                    Email = txtEmail.Text.Trim()
                };


                controller.SalvarCliente(c);
                MessageBox.Show("Cliente cadastrado com sucesso!");
            }
            else
            {
                // Atualização
                clienteEditando.Nome = txtNome.Text;
                clienteEditando.Endereco = txtEndereco.Text;
                clienteEditando.CPF_CNPJ = Masks.Unmask(txtCPF_CNPJ.Text);
                clienteEditando.Telefone = Masks.Unmask(txtTelefone.Text);
                clienteEditando.Email = txtEmail.Text;
                controller.EditarCliente(clienteEditando);
                MessageBox.Show("Cliente atualizado com sucesso!");
            }

            // Voltar à lista
            var clienteLista = new ClienteLista();
            clienteLista.Show();
            this.Close();
        }

        private bool _isMasking = false;

        private void txtCPF_CNPJ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isMasking) return;
            _isMasking = true;

            var tb = sender as TextBox;
            string original = tb.Text;
            int cursor = tb.SelectionStart;

            string masked = Utils.Masks.MaskCpfOrCnpj(original);

            tb.Text = masked;
            tb.SelectionStart = masked.Length;

            _isMasking = false;
        }

        private void txtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isMasking) return;
            _isMasking = true;

            var tb = sender as TextBox;
            string original = tb.Text;

            string masked = Utils.Masks.MaskPhone(original);

            tb.Text = masked;
            tb.SelectionStart = masked.Length;

            _isMasking = false;
        }

        private void TxtCPF_CNPJ_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // permitir somente números
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void TxtCPF_CNPJ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isMasking) return;
            _isMasking = true;

            var tb = sender as TextBox;
            string original = tb.Text;
            string masked = Utils.Masks.MaskCpfOrCnpj(original);

            tb.Text = masked;
            tb.SelectionStart = masked.Length;

            _isMasking = false;
        }

        private void TxtTelefone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void TxtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isMasking) return;
            _isMasking = true;

            var tb = sender as TextBox;
            string original = tb.Text;

            string masked = Utils.Masks.MaskPhone(original);

            tb.Text = masked;
            tb.SelectionStart = masked.Length;

            _isMasking = false;
        }


    }
}
