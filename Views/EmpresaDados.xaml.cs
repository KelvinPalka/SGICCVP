using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; // Necessário para classes de interface (Window, MessageBox, RoutedEventArgs)
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Projeto_BD.Models; // Importa o namespace que contém os modelos Empresa e Usuario
using WPF_Projeto_BD.Controllers; // Importa o namespace que contém o EmpresaController

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    /// <summary>
    /// Tela que exibe os dados da empresa
    /// </summary>
    public partial class EmpresaDados : Window
    {
        private Usuario usuarioLogado; // Usuário atualmente logado
        private int idEmpresa; // ID da empresa relacionada ao usuário
        private EmpresaController controller; // Controller responsável por gerenciar dados da empresa

        // Construtor da tela, recebe o usuário logado
        public EmpresaDados(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            usuarioLogado = usuario; // Armazena o usuário logado
            idEmpresa = usuarioLogado.IdEmpresa; // Armazena o ID da empresa
            controller = new EmpresaController(usuarioLogado); // Inicializa o controller passando o usuário
            CarregarEmpresa(); // Preenche a tela com os dados da empresa
        }

        // Método que carrega os dados da empresa nos labels
        private void CarregarEmpresa()
        {
            Empresa empresa = controller.ObterEmpresa(usuarioLogado.IdEmpresa); // Busca a empresa pelo ID do usuário

            if (empresa == null) // Verifica se a empresa foi encontrada
            {
                MessageBox.Show("Nenhuma empresa encontrada.");
                return;
            }

            // Preenche os labels da interface com os dados da empresa
            lblCNPJ.Text = empresa.CNPJ;
            lblNomeFantasia.Text = empresa.Nome_fantasia;
            lblRazaoSocial.Text = empresa.Razao_social;
            lblEmail.Text = empresa.Email;
            lblTelefone.Text = empresa.Telefone;
            lblEndereco.Text = empresa.Endereco;
        }

        // Evento do botão "Editar", abre a tela de edição da empresa
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            EmpresaEditar editarWindow = new EmpresaEditar(usuarioLogado, idEmpresa); // Passa usuário logado e ID da empresa
            editarWindow.Show(); // Abre a tela de edição
            this.Close(); // Fecha a tela atual
        }

        // Evento do botão "Voltar", retorna para a tela de configurações
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Config configWindow = new Config(usuarioLogado); // Cria a tela de configurações
            configWindow.Show(); // Abre a tela de configurações
            this.Close(); // Fecha a tela atual
        }
    }
}

/*
Resumo técnico:
- EmpresaDados é a View responsável por exibir os dados de uma empresa.
- Segue o padrão MVC + DAO: toda lógica de obtenção e manipulação dos dados é delegada ao EmpresaController.
- Recebe o usuário logado no construtor, garantindo que apenas dados da empresa vinculada sejam carregados.
- CarregarEmpresa() preenche os labels da interface com informações da empresa.
- BtnEditar_Click abre a tela de edição passando o usuário logado e o ID da empresa.
- BtnVoltar_Click retorna para a tela de configurações.
- Nenhuma lógica de negócio ou acesso direto a banco ocorre na View; tudo é feito pelo controller.
*/
