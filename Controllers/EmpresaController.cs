using System; // Importa classes básicas do .NET
using System.Windows; // Importa classes para interação com WPF (MessageBox, Window, etc.)
using WPF_Projeto_BD.Models; // Importa os modelos do projeto (Empresa, Usuario)
using WPF_Projeto_BD.Data.DAO; // Importa os DAOs para acesso a dados
using WPF_Projeto_BD.Views; // Importa as Views (telas) do projeto

namespace WPF_Projeto_BD.Controllers // Define o namespace dos controllers
{
    public class EmpresaController // Controller responsável pela lógica de negócios de Empresa
    {
        private readonly EmpresaDAO empresaDAO = new EmpresaDAO(); // DAO para operações com empresas
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO(); // DAO para operações com usuários
        private readonly Usuario usuarioLogado; // Usuário logado para controle de acesso e navegação

        // Construtor sem usuário (para telas iniciais que não exigem login)
        public EmpresaController()
        {
        }

        // Construtor com usuário (para telas que exigem login)
        public EmpresaController(Usuario usuario)
        {
            usuarioLogado = usuario; // Inicializa a variável com o usuário logado
        }

        // =====================================================
        // CADASTRAR EMPRESA + ADMINISTRADOR
        // =====================================================
        public void CadastrarEmpresaComAdministrador( // Método para cadastrar empresa e administrador
            string cnpj, string nomeFantasia, string emailEmpresa, string telefone, // Dados da empresa
            string razaoSocial, string endereco, // Mais dados da empresa
            string nomeADM, string emailADM, string senhaADM, string confirmSenhaADM) // Dados do administrador
        {
            if (senhaADM != confirmSenhaADM) // Verifica se as senhas coincidem
                throw new Exception("As senhas não conferem!"); // Lança exceção se não conferirem

            if (empresaDAO.ExisteCNPJ(cnpj)) // Verifica se o CNPJ já está cadastrado
                throw new Exception("Já existe uma empresa cadastrada com este CNPJ."); // Lança exceção se existir

            if (usuarioDAO.ExisteEmail(emailADM)) // Verifica se o e-mail do admin já existe
                throw new Exception("Já existe um usuário com este e-mail de administrador."); // Lança exceção se existir

            if (empresaDAO.ExisteRazaoSocial(razaoSocial)) // Verifica se a razão social já existe
                throw new Exception("Já existe uma empresa cadastrada com esta Razão Social."); // Lança exceção se existir

            // Cria a instância da empresa com os dados informados
            Empresa empresa = new Empresa
            {
                CNPJ = cnpj, // CNPJ da empresa
                Nome_fantasia = nomeFantasia, // Nome fantasia da empresa
                Email = emailEmpresa, // Email da empresa
                Telefone = telefone, // Telefone da empresa
                Razao_social = razaoSocial, // Razão social da empresa
                Endereco = endereco // Endereço da empresa
            };

            int idEmpresa = empresaDAO.Inserir(empresa); // Insere a empresa no banco e retorna o ID

            // Cria o administrador da empresa
            Usuario usuario = new Usuario
            {
                Nome = nomeADM, // Nome do administrador
                Email = emailADM, // Email do administrador
                SenhaHash = senhaADM, // Hash será gerado pelo DAO
                TipoUsuario = "admin", // Tipo de usuário é admin
                IdEmpresa = idEmpresa // Associa o administrador à empresa criada
            }; 

            usuarioDAO.Inserir(usuario); // Insere o administrador no banco

            // Mensagem de sucesso
            MessageBox.Show("Empresa e administrador cadastrados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // =====================================================
        // OBTER EMPRESA
        // =====================================================
        public Empresa ObterEmpresa(int idEmpresa)
        {
            try // Tenta obter a empresa pelo ID
            {
                return empresaDAO.ObterEmpresaPorId(idEmpresa); // Retorna a empresa encontrada
            }
            catch (Exception ex) // Captura exceções
            {
                MessageBox.Show("Erro ao obter dados da empresa: " + ex.Message, // Exibe mensagem de erro
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error); // Mensagem de erro
                return null; // Retorna nulo em caso de erro
            }
        }

        // =====================================================
        // EDITAR EMPRESA
        // =====================================================
        public bool EditarEmpresa(Empresa empresa) // Método para editar os dados da empresa
        {
            try // Tenta editar a empresa
            {
                if (empresa == null) // Verifica se a empresa é válida
                    throw new Exception("Empresa inválida."); // Lança exceção se não for válida

                return empresaDAO.Editar(empresa); // Chama DAO para editar os dados da empresa
            }
            catch (Exception ex) // Captura exceções
            {
                MessageBox.Show("Erro ao editar empresa: " + ex.Message, // Exibe mensagem de erro
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error); // Mensagem de erro
                return false; // Retorna false em caso de falha
            }
        }

        // =====================================================
        // VALIDAÇÃO DE SENHA
        // =====================================================
        public bool SenhaValida(string senha) // Método para validar a senha do administrador
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 6) // Verifica se a senha é válida (mínimo 6 caracteres)
                return false; // Retorna false se inválida

            bool temLetra = false; // Marca se há pelo menos uma letra
            bool temNumero = false; // Marca se há pelo menos um número

            foreach (char c in senha) // Percorre cada caractere da senha
            {
                if (char.IsLetter(c)) temLetra = true; // Marca se houver letra
                if (char.IsDigit(c)) temNumero = true; // Marca se houver número
            }

            return temLetra && temNumero; // Retorna true se houver pelo menos uma letra e um número
        }

        // =====================================================
        // NAVEGAÇÃO PARA HOME
        // =====================================================
        public void VoltarHome(Window telaAtual) // Método para voltar à tela Home
        {
            if (usuarioLogado == null) // Verifica se o usuário está logado
            {
                MessageBox.Show("Usuário não definido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning); // Mensagem de erro
                return; // Sai do método
            }

            new Home(usuarioLogado).Show(); // Abre a tela Home com o usuário logado
            telaAtual.Close(); // Fecha a tela atual
        }
    }
}

/*
EmpresaController gerencia a lógica de negócios relacionada a empresas no sistema.
- Cadastra empresas e seus administradores, garantindo unicidade de CNPJ, razão social e e-mail do administrador.
- Permite obter e editar informações da empresa através do EmpresaDAO.
- Valida senhas de administradores (mínimo 6 caracteres, pelo menos uma letra e um número).
- Controla a navegação para a tela Home, considerando o usuário logado.
- Trata exceções e exibe mensagens de erro detalhadas para a interface, garantindo consistência e confiabilidade no fluxo de dados.
*/
