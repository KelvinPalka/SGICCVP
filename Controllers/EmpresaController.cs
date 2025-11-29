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

        // =====================================================
        //  CADASTRAR EMPRESA + USUÁRIO ADMINISTRADOR
        // =====================================================
        public void CadastrarEmpresaComAdministrador(
            string cnpj, string nomeFantasia, string emailEmpresa, string telefone,
            string razaoSocial, string endereco,
            string nomeADM, string emailADM, string senhaADM, string confirmSenhaADM)
        {
            // Validação
            if (senhaADM != confirmSenhaADM)
                throw new Exception("As senhas não conferem!");

            // Monta o objeto empresa
            Empresa empresa = new Empresa
            {
                CNPJ = cnpj,
                Nome_fantasia = nomeFantasia,
                Email = emailEmpresa,
                Telefone = telefone,
                Razao_social = razaoSocial,
                Endereco = endereco
            };

            // 1. Inserir a empresa e pegar o ID gerado
            int idEmpresa = empresaDAO.Inserir(empresa);

            // 2. Criar o administrador
            Usuario usuario = new Usuario
            {
                Nome = nomeADM,
                Email = emailADM,
                Senha = senhaADM,
                TipoUsuario = "admin",
                IdEmpresa = idEmpresa
            };

            usuarioDAO.Inserir(usuario);

            // 3. Feedback
            MessageBox.Show("Empresa e administrador cadastrados com sucesso!",
                            "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            // 4. Abrir login
            var login = new Login();
            login.Show();

            // 5. Fechar a tela EmpresaCadastro
            foreach (Window w in Application.Current.Windows)
            {
                if (w is EmpresaCadastro)
                {
                    w.Close();
                    break;
                }
            }
        }

        // =====================================================
        //  OBTER EMPRESA (para tela de visualização/edição)
        // =====================================================
        public Empresa ObterEmpresa(int idEmpresa)
        {
            return empresaDAO.ObterEmpresaPorId(idEmpresa);
        }

        public bool EditarEmpresa(Empresa empresa) 
        {
            if (empresa == null) 
                return false;
            
            return empresaDAO.Editar(empresa);
        }
    }
}
