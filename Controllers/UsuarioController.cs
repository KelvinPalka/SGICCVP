using System;
using System.Collections.Generic;
using System.Linq;
using WPF_Projeto_BD.Data.DAO;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Utils;

namespace WPF_Projeto_BD.Controllers
{
    internal class UsuarioController
    {
        private readonly UsuarioDAO usuarioDao = new UsuarioDAO();

        // =========================
        // Cadastrar usuário (manual)
        // =========================
        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa)
        {
            return CadastrarUsuario(nome, email, senha, tipoUsuario, idEmpresa, false);
        }

        // =========================
        // Cadastrar usuário com opção de senha apenas numérica (automatico)
        // =========================
        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa, bool permitirApenasNumeros)
        {
            // 1) Validar campos obrigatórios
            if (string.IsNullOrWhiteSpace(nome))
                return "O nome é obrigatório.";

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return "E-mail inválido.";

            if (!SenhaValida(senha, permitirApenasNumeros))
                return permitirApenasNumeros
                    ? "Senha inválida."
                    : "A senha deve ter pelo menos 6 caracteres, incluindo letras e números.";

            // 2) Verificar duplicidade de e-mail
            if (usuarioDao.ExisteEmail(email))
                return "Este e-mail já está cadastrado.";

            // 3) Gerar hash + salt
            var (hash, salt) = HashService.GerarHashComSalt(senha);

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = hash,
                Salt = salt,
                TipoUsuario = tipoUsuario,
                IdEmpresa = idEmpresa
            };

            // 4) Inserir no banco
            usuarioDao.Inserir(usuario);

            return "ok";
        }

        // =========================
        // Obter todos os usuários
        // =========================
        public List<Usuario> ObterTodos(int idEmpresa)
        {
            return usuarioDao.ObterTodos(idEmpresa);
        }

        // =========================
        // Atualizar usuário
        // =========================
        public string AtualizarUsuario(Usuario usuario, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                return "O nome é obrigatório.";

            if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
                return "E-mail inválido.";

            if (string.IsNullOrWhiteSpace(usuario.TipoUsuario) ||
                (usuario.TipoUsuario != "admin" && usuario.TipoUsuario != "user"))
                return "O tipo de conta deve ser 'admin' ou 'user'.";

            // ================= VALIDAÇÃO DE SENHA =================
            if (string.IsNullOrWhiteSpace(novaSenha))
            {
                if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
                    return "A senha não pode ficar vazia.";
                // Senha antiga permanece
            }
            else
            {
                if (!SenhaValida(novaSenha, false))
                    return "A senha deve ter pelo menos 6 caracteres, com letras e números.";

                // Atualiza hash e salt
                var resultado = HashService.GerarHashComSalt(novaSenha);
                usuario.SenhaHash = resultado.hash;
                usuario.Salt = resultado.salt;
            }

            // Verifica duplicidade de e-mail
            var todosUsuarios = usuarioDao.ObterTodos(usuario.IdEmpresa);
            foreach (var u in todosUsuarios)
            {
                if (u.Email == usuario.Email && u.IdUsuario != usuario.IdUsuario)
                    return "Este e-mail já está cadastrado por outro usuário.";
            }

            bool sucesso = usuarioDao.Atualizar(usuario);
            return sucesso ? "ok" : "erro";
        }


        // =========================
        // Excluir usuário
        // =========================
        public bool ExcluirUsuario(int idUsuario)
        {
            return usuarioDao.Excluir(idUsuario);
        }

        // =========================
        // Obter usuário por ID
        // =========================
        public Usuario ObterPorId(int idUsuario)
        {
            return usuarioDao.ObterPorId(idUsuario);
        }

        // =========================
        // Gerar PDF (futuro)
        // =========================
        public void GerarPDF()
        {
            // Implementação futura
        }

        // =========================
        // Validação de senha
        // =========================
        private bool SenhaValida(string senha, bool permitirApenasNumeros)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                return false;

            if (permitirApenasNumeros)
                return true; // permite CPF numérico para cadastro automático

            bool temLetra = senha.Any(char.IsLetter);
            bool temNumero = senha.Any(char.IsDigit);

            return temLetra && temNumero;
        }
    }
}
