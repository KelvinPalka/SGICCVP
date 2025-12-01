using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using WPF_Projeto_BD.Models; // Importa os modelos Empresa e Usuario
using WPF_Projeto_BD.Controllers; // Importa o controller EmpresaController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela para editar os dados de uma empresa existente
    /// </summary>
    public partial class EmpresaEditar : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado
        private int idEmpresa; // ID da empresa a ser editada
        private Empresa empresaAtual; // Objeto Empresa carregado para edição
        private EmpresaController controller; // Controller responsável pela lógica de edição

        // Construtor da tela, recebe o usuário logado e o ID da empresa
        public EmpresaEditar(Usuario usuario, int id_empresa)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
            idEmpresa = id_empresa; // Armazena o ID da empresa
            controller = new EmpresaController(usuarioLogado); // Inicializa o controller
            ObterEmpresaPorId(); // Carrega os dados atuais da empresa nos TextBox
        }

        // Método que obtém a empresa pelo ID e preenche os campos
        private void ObterEmpresaPorId()
        {
            empresaAtual = controller.ObterEmpresa(idEmpresa); // Busca a empresa pelo ID

            if (empresaAtual == null) // Verifica se a empresa foi encontrada
            {
                MessageBox.Show("Empresa não encontrada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Preenche os TextBox com os dados atuais da empresa
            txtCNPJ.Text = empresaAtual.CNPJ;
            txtNomeFantasia.Text = empresaAtual.Nome_fantasia;
            txtRazaoSocial.Text = empresaAtual.Razao_social;
            txtEmail.Text = empresaAtual.Email;
            txtTelefone.Text = empresaAtual.Telefone;
            txtEndereco.Text = empresaAtual.Endereco;
        }

        // Evento do botão "Salvar"
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Atualiza o objeto empresaAtual com os valores digitados nos TextBox
            empresaAtual.CNPJ = txtCNPJ.Text;
            empresaAtual.Nome_fantasia = txtNomeFantasia.Text;
            empresaAtual.Razao_social = txtRazaoSocial.Text;
            empresaAtual.Email = txtEmail.Text;
            empresaAtual.Telefone = txtTelefone.Text;
            empresaAtual.Endereco = txtEndereco.Text;

            bool sucesso = controller.EditarEmpresa(empresaAtual); // Chama o controller para atualizar no banco

            if (sucesso) // Se atualização foi bem-sucedida
            {
                MessageBox.Show("Dados da empresa atualizados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Volta para a tela de visualização dos dados da empresa
                EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
                empresaDadosWindow.Show();
                this.Close();
            }
            else // Caso ocorra algum erro
            {
                MessageBox.Show("Erro ao atualizar os dados da empresa.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Evento do botão "Cancelar", volta para tela de visualização
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
            empresaDadosWindow.Show();
            this.Close();
        }

        // Evento do botão "Voltar", também retorna à tela de visualização
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            EmpresaDados empresaDadosWindow = new EmpresaDados(usuarioLogado);
            empresaDadosWindow.Show();
            this.Close();
        }
    }
}

/*
Resumo técnico:
- EmpresaEditar é a View responsável por permitir a edição dos dados de uma empresa existente.
- Segue o padrão MVC + DAO: toda lógica de obtenção e atualização dos dados é delegada ao EmpresaController.
- Recebe o usuário logado e o ID da empresa no construtor, garantindo que apenas dados da empresa vinculada sejam carregados.
- ObterEmpresaPorId() preenche os TextBox com os dados atuais da empresa.
- BtnSalvar_Click atualiza o objeto Empresa e chama o controller para persistir as alterações.
- BtnCancelar_Click e BtnVoltar_Click retornam para a tela de visualização EmpresaDados.
- Nenhuma lógica de negócio ou acesso direto ao banco ocorre na View; tudo é tratado pelo controller.
*/
