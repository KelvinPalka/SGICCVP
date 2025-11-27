using System.Windows;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Views;

namespace WPF_Projeto_BD.Controllers
{
    public class EmpresaController
    {
        public void CadastrarEmpresaComAdministrador(
            string cnpj,
            string nomeFantasia,
            string emailEmpresa,
            string telefone,
            string razao,
            string endereco,
            string nomeAdm,
            string emailAdm,
            string senhaAdm,
            string senhaConfirm)
        {
            // validações simples
            if (senhaAdm != senhaConfirm)
            {
                MessageBox.Show("As senhas não coincidem!");
                return;
            }

            // cria objetos
            var empresa = new Empresa
            {
                CNPJ = cnpj,
                Nome_fantasia = nomeFantasia,
                Email = emailEmpresa,
                Telefone = telefone,
                Razao_social = razao,
                Endereco = endereco
            };

            var empresaDAO = new EmpresaDAO();
            empresaDAO.Inserir(empresa);

            var usuario = new Usuario
            {
                Nome = nomeAdm,
                Email = emailAdm,
                Senha = senhaAdm,
                IdEmpresa = empresa.Id
            };

            var usuarioDAO = new UsuarioDAO();
            usuarioDAO.Inserir(usuario);

            MessageBox.Show("Empresa + ADM cadastrados com sucesso!");

            // navegação (controller controla a troca de tela)
            var tela = new MainWindow();
            tela.Show();
        }
    }
}
