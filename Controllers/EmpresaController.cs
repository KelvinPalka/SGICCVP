using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Controllers
{
    public class EmpresaController
    {
        private EmpresaDAO empresaDAO = new EmpresaDAO();
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        public void CadastrarEmpresaComAdministrador(
            string cnpj, string nomeFantasia, string emailEmpresa, string telefone, string razaoSocial, string endereco,
            string nomeADM, string emailADM, string senhaADM, string confirmSenhaADM)
        {
            // Valida campos obrigatórios
            if (string.IsNullOrEmpty(cnpj) || string.IsNullOrEmpty(nomeFantasia) || string.IsNullOrEmpty(nomeADM))
                throw new Exception("Preencha todos os campos obrigatórios.");

            if (senhaADM != confirmSenhaADM)
                throw new Exception("As senhas não conferem.");

            // Aplica máscaras
            cnpj = Utils.Masks.Unmask(cnpj);
            telefone = Utils.Masks.Unmask(telefone);

            //Cria objeto Empresa
            Empresa empresa = new Empresa
            {
                CNPJ = cnpj,
                Nome_fantasia = nomeFantasia,
                Email = emailEmpresa,
                Telefone = telefone,
                Razao_social = razaoSocial,
                Endereco = endereco
            };

            //Insere empresa no banco
            empresaDAO.Inserir(empresa);

            //Cria o usuário administrador vinculado à empresa
            Usuario usuario = new Usuario
            {
                Nome = nomeADM,
                Email = emailADM,
                Senha = senhaADM,
                TipoUsuario = "Administrador",
                IdEmpresa = empresa.Id
            };

            // Insere usuário no banco
            usuarioDAO.Inserir(usuario);
        }
    }

}
