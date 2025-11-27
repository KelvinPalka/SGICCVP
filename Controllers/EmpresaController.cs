using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Views;
using System;

namespace WPF_Projeto_BD.Controllers
{
    public class EmpresaController
    {
        public void CadastrarEmpresaComAdministrador(
            string cnpj, string nomeFantasia, string emailEmpresa, string telefone,
            string razaoSocial, string endereco,
            string nomeADM, string emailADM, string senhaADM, string confirmSenhaADM)
        {
            // Validação simples
            if (senhaADM != confirmSenhaADM)
                throw new Exception("As senhas não conferem!");

            // Monta empresa
            Empresa empresa = new Empresa
            {
                CNPJ = cnpj,
                Nome_fantasia = nomeFantasia,
                Email = emailEmpresa,
                Telefone = telefone,
                Razao_social = razaoSocial,
                Endereco = endereco
            };

            var empresaDAO = new EmpresaDAO();
            var usuarioDAO = new UsuarioDAO();

            // 1. Insere empresa e obtém o ID
            int idEmpresa = empresaDAO.Inserir(empresa);

            // 2. Cria usuário administrador
            Usuario usuario = new Usuario
            {
                Nome = nomeADM,
                Email = emailADM,
                Senha = senhaADM,
                TipoUsuario = "admin", // <-- propriedade correta
                IdEmpresa = idEmpresa
            };

            usuarioDAO.Inserir(usuario);

            // 3. Notifica e abre login
            MessageBox.Show("Empresa e administrador cadastrados com sucesso!",
                            "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            // Abre tela de login
            var login = new Login();   // coloque o nome correto da sua tela aqui
            login.Show();

            // Fecha a tela atual (EmpresaCadastro)
            foreach (Window w in Application.Current.Windows)
            {
                if (w is EmpresaCadastro)
                {
                    w.Close();
                    break;
                }
            }
        }
    }
}
