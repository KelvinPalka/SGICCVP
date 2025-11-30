using System.Collections.Generic;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Controllers
{
    internal class UsuarioController
    {

        private UsuarioDAO dao = new UsuarioDAO();

        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa)
        {
            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                TipoUsuario = tipoUsuario,
                IdEmpresa = idEmpresa
            };

            dao.Inserir(usuario);
            return "ok";
        }

        public List<Usuario> ObterTodos(int idEmpresa)
        {
            return dao.ObterTodos(idEmpresa);
        }

        public bool EditarUsuario(Usuario usuario)
        {
            return dao.Editar(usuario);
        }

        public bool ExcluirUsuario(int idUsuario)
        {
            return dao.Excluir(idUsuario);
        }

        public Usuario ObterPorId(int idUsuario)
        {
            return dao.ObterPorId(idUsuario);
        }
    }
}
