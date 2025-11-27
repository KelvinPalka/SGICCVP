using System.Text.RegularExpressions;
using System.Windows;
using WPF_Projeto_BD.Controllers;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Views
{
    public partial class ClienteCadastro : Window
    {
        private readonly ClienteController _controller; // Controlador de clientes
        private Cliente clienteEditando; // Cliente que está sendo editado (nulo se for um novo cliente)

        public ClienteCadastro(ClienteController controller) // Construtor para novo cliente
        {
            InitializeComponent();
            _controller = controller; // Atribui o controlador
        }

        public ClienteCadastro(Cliente cliente, ClienteController controller) // Construtor para editar cliente existente
        {
            InitializeComponent();
            _controller = controller; // Atribui o controlador
            clienteEditando = cliente; // Atribui o cliente que está sendo editado

            txtNome.Text = cliente.Nome;
            txtCPF_CNPJ.Text = cliente.CPF_CNPJ;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim(); 
            string cpf = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", "");
            string end = txtEndereco.Text.Trim();
            string tel = Regex.Replace(txtTelefone.Text, "[^0-9]", "");
            string email = txtEmail.Text.Trim();

            string resposta;

            if (clienteEditando == null)
            {
                resposta = _controller.Salvar(nome, cpf, end, tel, email);
            }
            else
            {
                resposta = _controller.Editar(clienteEditando, nome, cpf, end, tel, email);
            }

            if (resposta != "OK")
            {
                MessageBox.Show(resposta);
                return;
            }

            new ClienteLista(_controller).Show();
            Close();
        }
    }
}
