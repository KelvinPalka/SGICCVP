using System; // Importa namespaces essenciais do C#
using System.Text.RegularExpressions; // Necessário para manipular CPF/CNPJ e telefone
using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Controllers; // Importa o namespace que contém o ClienteController
using WPF_Projeto_BD.Models; // Importa o namespace que contém o modelo Cliente

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Tela de cadastro e edição de cliente
    public partial class ClienteCadastro : Window
    {
        private readonly ClienteController _controller; // Controller responsável por gerenciar clientes
        private Cliente clienteEditando; // Se não for nulo, indica que a tela está em modo de edição

        // Construtor para cadastrar um novo cliente
        public ClienteCadastro(ClienteController controller)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            _controller = controller; // Atribui a referência do controller recebido
        }

        // Construtor para editar um cliente existente
        public ClienteCadastro(Cliente cliente, ClienteController controller)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            _controller = controller; // Atribui o controller
            clienteEditando = cliente; // Guarda o cliente que será editado

            // Preenche os campos com os dados do cliente existente
            txtNome.Text = cliente.Nome;
            txtCPF_CNPJ.Text = cliente.CPF_CNPJ;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;

            btnSalvar.Content = "Atualizar Cliente"; // Altera o texto do botão para refletir edição
        }

        // Evento do botão Salvar
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Captura e limpa os valores digitados pelo usuário
            string nome = txtNome.Text.Trim();
            string cpf = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", ""); // Remove todos os caracteres que não são números
            string end = txtEndereco.Text.Trim();
            string tel = Regex.Replace(txtTelefone.Text, "[^0-9]", ""); // Remove tudo que não for número
            string email = txtEmail.Text.Trim();

            string resposta; // Variável para receber retorno do controller (validação ou sucesso)

            try
            {
                // Verifica se é cadastro novo ou edição
                if (clienteEditando == null)
                    resposta = _controller.Salvar(nome, cpf, end, tel, email); // Novo cadastro
                else
                    resposta = _controller.Editar(clienteEditando, nome, cpf, end, tel, email); // Edição de cliente existente

                // Se houver algum problema de validação, mostra aviso e não fecha a tela
                if (resposta != "OK")
                {
                    MessageBox.Show(resposta, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Cadastro ou edição realizados com sucesso
                new ClienteLista(_controller).Show(); // Abre a tela de listagem de clientes
                Close(); // Fecha a tela atual
            }
            catch (Exception ex)
            {
                // Captura qualquer erro inesperado e exibe mensagem de erro
                MessageBox.Show(
                    "Ocorreu um erro inesperado: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}

/*
Resumo técnico:
- ClienteCadastro é a View responsável por cadastrar e editar clientes.
- Segue o padrão MVC + DAO: toda lógica de validação e salvamento é delegada ao ClienteController.
- Possui dois construtores: um para novo cliente e outro para edição.
- Campos de CPF/CNPJ e telefone são tratados com Regex para aceitar apenas números.
- Evento BtnSalvar_Click decide entre salvar ou editar, trata validações e exibe mensagens de erro/aviso.
- Ao concluir a operação com sucesso, a tela de listagem de clientes é aberta.
- Nenhuma lógica de negócio é implementada na View; tudo é feito pelo controller.
*/
