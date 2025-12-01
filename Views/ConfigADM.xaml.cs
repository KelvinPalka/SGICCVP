using System.Windows; // Necessário para classes de interface (Window, RoutedEventArgs)
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers; // Importa o namespace que contém o ConfigController
using WPF_Projeto_BD.Models; // Importa o namespace que contém o modelo Usuario

namespace WPF_Projeto_BD.Views // Define o namespace da aplicação (Views)
{
    // Tela de configuração do sistema
    public partial class Config : Window
    {
        private readonly ConfigController controller; // Controller responsável por gerenciar ações da tela de configuração

        // Construtor da tela, recebe o usuário logado
        public Config(Usuario usuario)
        {
            InitializeComponent(); // Inicializa os componentes visuais
            controller = new ConfigController(usuario); // Cria o controller passando o usuário atual
        }

        // Evento do botão "Voltar" para retornar à tela inicial
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirHome(this); // Chama método do controller para abrir a tela inicial e fechar a atual
        }

        // Evento do botão "Dados da Empresa"
        private void BtnDadosEmpresa_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirEmpresaDados(this); // Abre tela de dados da empresa via controller
        }

        // Evento do botão "Cadastrar Usuário"
        private void BtnUsuarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirUsuarioCadastro(this); // Abre tela de cadastro de usuário
        }

        // Evento do botão "Lista de Usuários"
        private void BtnUsuarioLista_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirUsuarioLista(this); // Abre tela de listagem de usuários
        }

        // Evento do botão "Cadastrar Funcionário"
        private void BtnFuncionarioCadastrar_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirFuncionarioCadastro(this); // Abre tela de cadastro de funcionário
        }

        // Evento do botão "Lista de Funcionários"
        private void BtnFuncionarioLista_Click(object sender, RoutedEventArgs e)
        {
            controller.AbrirFuncionarioLista(this); // Abre tela de listagem de funcionários
        }

        // Evento do botão "Materiais" (ainda não implementado)
        private void BtnMateriais_Click(object sender, RoutedEventArgs e)
        {
            // Futuramente, poderá chamar métodos do controller relacionados a materiais
        }

        // OBS: Outros botões (backup, preferências, redefinir senha) ainda não implementados podem seguir o mesmo padrão
    }
}

/*
Resumo técnico:
- Config é a View responsável por gerenciar as opções de configuração do sistema.
- Segue o padrão MVC + DAO: toda a lógica de abertura de telas e ações administrativas é delegada ao ConfigController.
- Recebe o usuário logado no construtor, garantindo que ações administrativas respeitem permissões.
- Cada botão possui um evento que chama o controller correspondente, mantendo a View livre de lógica de negócio.
- Botões ainda não implementados (Materiais, Backup, Preferências, Redefinir senha) podem ser facilmente integrados chamando métodos do controller.
- A tela garante separação clara de responsabilidades: View apenas interage visualmente, Controller gerencia operações.
*/
