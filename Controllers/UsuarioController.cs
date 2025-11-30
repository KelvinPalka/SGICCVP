using System.Collections.Generic;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Utils; // onde deve estar seu HashService
using System.Diagnostics;

namespace WPF_Projeto_BD.Controllers
{
    internal class UsuarioController
    {
        private UsuarioDAO dao = new UsuarioDAO();

        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa)
        {
            // 1. Gera hash + salt
            var (hash, salt) = HashService.GerarHashComSalt(senha);

            System.Diagnostics.Debug.WriteLine("HASH GERADO: " + hash);
            System.Diagnostics.Debug.WriteLine("SALT GERADO: " + salt);

            // 2. Cria o usuário com os nomes CERTOS
            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = hash,
                Salt = salt,
                TipoUsuario = tipoUsuario,
                IdEmpresa = idEmpresa
            };

            // 3. Envia para o DAO
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

        public void GerarPDF()
        {
            // Implementação futura
        }
    }
}
