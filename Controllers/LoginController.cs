using WPF_Projeto_BD.Data.DAO; // Importa o DAO de usuários para acesso ao banco

namespace WPF_Projeto_BD.Controllers
{
    public class LoginController // Controller responsável pela autenticação de usuários
    {
        private readonly UsuarioDAO usuarioDao = new UsuarioDAO(); // DAO para operações de login e consulta de usuários

        public Usuario Autenticar(string email, string senha) // Método para autenticar usuário
        {
            return usuarioDao.Login(email, senha);
            // Chama o DAO para verificar email e senha no banco
            // Retorna um objeto Usuario se encontrado, ou null se não autenticado
        }
    }
}

/*
LoginController gerencia a autenticação de usuários no sistema.
- Utiliza UsuarioDAO para validar email e senha no banco de dados.
- Retorna o objeto Usuario correspondente em caso de sucesso ou null caso falhe.
- Centraliza a lógica de login, separando a autenticação da interface (View).
*/
