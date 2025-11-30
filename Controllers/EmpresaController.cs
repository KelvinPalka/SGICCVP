using System;
using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Controllers
{
    public class EmpresaController
    {
        private readonly EmpresaDAO empresaDAO = new EmpresaDAO();
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        private readonly Usuario usuarioLogado;

        // Construtor sem usuário (para telas iniciais)
        public EmpresaController()
        {
        }

        // Construtor com usuário (para telas que exigem login)
        public EmpresaController(Usuario usuario)
        {
            usuarioLogado = usuario;
        }

        // =====================================================
        // CADASTRAR EMPRESA + ADMINISTRADOR
        // =====================================================
        public void CadastrarEmpresaComAdministrador(
            string cnpj, string nomeFantasia, string emailEmpresa, string telefone,
            string razaoSocial, string endereco,
            string nomeADM, string emailADM, string senhaADM, string confirmSenhaADM)
        {
            if (senhaADM != confirmSenhaADM)
                throw new Exception("As senhas não conferem!");

            // Valida CNPJ único
            if (empresaDAO.ExisteCNPJ(cnpj))
                throw new Exception("Já existe uma empresa cadastrada com este CNPJ.");

            // Valida email do admin único
            if (usuarioDAO.ExisteEmail(emailADM))
                throw new Exception("Já existe um usuário com este e-mail de administrador.");

            // Valida Razão Social única
            if (empresaDAO.ExisteRazaoSocial(razaoSocial))
            {
                throw new Exception("Já existe uma empresa cadastrada com esta Razão Social.");
            }


            // Cria empresa
            Empresa empresa = new Empresa
            {
                CNPJ = cnpj,
                Nome_fantasia = nomeFantasia,
                Email = emailEmpresa,
                Telefone = telefone,
                Razao_social = razaoSocial,
                Endereco = endereco
            };

            int idEmpresa = empresaDAO.Inserir(empresa);

            // Cria administrador
            Usuario usuario = new Usuario
            {
                Nome = nomeADM,
                Email = emailADM,
                SenhaHash = senhaADM, // hash será gerado pelo DAO
                TipoUsuario = "admin",
                IdEmpresa = idEmpresa
            };

            usuarioDAO.Inserir(usuario);

            MessageBox.Show("Empresa e administrador cadastrados com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // =====================================================
        // OBTER EMPRESA
        // =====================================================
        public Empresa ObterEmpresa(int idEmpresa)
        {
            try
            {
                return empresaDAO.ObterEmpresaPorId(idEmpresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter dados da empresa: " + ex.Message,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        // =====================================================
        // EDITAR EMPRESA
        // =====================================================
        public bool EditarEmpresa(Empresa empresa)
        {
            try
            {
                if (empresa == null)
                    throw new Exception("Empresa inválida.");

                return empresaDAO.Editar(empresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar empresa: " + ex.Message,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // =====================================================
        // VALIDAÇÃO DE SENHA
        // =====================================================
        public bool SenhaValida(string senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 6)
                return false;

            bool temLetra = false;
            bool temNumero = false;

            foreach (char c in senha)
            {
                if (char.IsLetter(c)) temLetra = true;
                if (char.IsDigit(c)) temNumero = true;
            }

            return temLetra && temNumero;
        }

        // =====================================================
        // NAVEGAÇÃO PARA HOME
        // =====================================================
        public void VoltarHome(Window telaAtual)
        {
            if (usuarioLogado == null)
            {
                MessageBox.Show("Usuário não definido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            new Home(usuarioLogado).Show();
            telaAtual.Close();
        }
    }
}
