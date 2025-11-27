using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Controllers
{
    public class LoginController
    {
        private readonly UsuarioDAO usuarioDao = new UsuarioDAO();

        public Usuario Autenticar(string email, string senha)
        {
            return usuarioDao.Login(email, senha);
        }
    }
}
