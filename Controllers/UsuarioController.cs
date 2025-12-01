using System; // Importa classes básicas do .NET
using System.Collections.Generic; // Importa listas genéricas
using System.Linq; // Importa LINQ para consultas em coleções
using WPF_Projeto_BD.Data.DAO; // Importa os DAOs para acesso ao banco
using WPF_Projeto_BD.Utils; // Importa utilitários (HashService)

namespace WPF_Projeto_BD.Controllers
{
    internal class UsuarioController // Controller responsável pela lógica de negócios de usuários
    {
        private readonly UsuarioDAO usuarioDao = new UsuarioDAO(); // DAO para operações de usuários

        // =========================
        // Cadastrar usuário (manual)
        // =========================
        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa)
        {
            return CadastrarUsuario(nome, email, senha, tipoUsuario, idEmpresa, false);
            // Chama método sobrecarregado permitindo apenas senhas normais (não numéricas)
        }

        // =========================
        // Cadastrar usuário com opção de senha apenas numérica (automático)
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

            // 3) Gerar hash + salt da senha
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

            return "ok"; // Retorna "ok" se cadastro for bem-sucedido
        }

        // =========================
        // Obter todos os usuários de uma empresa
        // =========================
        public List<Usuario> ObterTodos(int idEmpresa)
        {
            return usuarioDao.ObterTodos(idEmpresa); // Retorna lista de usuários
        }

        // =========================
        // Atualizar usuário existente
        // =========================
        public string AtualizarUsuario(Usuario usuario, string novaSenha)
        {
            // Valida campos obrigatórios
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
                    return "A senha não pode ficar vazia."; // Senha antiga permanece se preenchida
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

            // Verifica duplicidade de e-mail entre outros usuários
            var todosUsuarios = usuarioDao.ObterTodos(usuario.IdEmpresa);
            foreach (var u in todosUsuarios)
            {
                if (u.Email == usuario.Email && u.IdUsuario != usuario.IdUsuario)
                    return "Este e-mail já está cadastrado por outro usuário.";
            }

            bool sucesso = usuarioDao.Atualizar(usuario);
            return sucesso ? "ok" : "erro"; // Operador ternário: retorna "ok" se true, "erro" se false
        }

        // =========================
        // Excluir usuário pelo ID
        // =========================
        public bool ExcluirUsuario(int idUsuario)
        {
            return usuarioDao.Excluir(idUsuario); // Remove usuário do banco
        }

        // =========================
        // Obter usuário por ID
        // =========================
        public Usuario ObterPorId(int idUsuario)
        {
            return usuarioDao.ObterPorId(idUsuario); // Retorna usuário correspondente ao ID
        }

        // =========================
        // Gerar PDF (implementação futura)
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
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6) // Valida mínimo 6 caracteres
                return false;

            if (permitirApenasNumeros)
                return true; // Permite apenas números (ex: CPF) para cadastro automático

            bool temLetra = senha.Any(char.IsLetter); // Verifica se há pelo menos uma letra
            bool temNumero = senha.Any(char.IsDigit); // Verifica se há pelo menos um número

            return temLetra && temNumero; // Retorna true se senha válida
        }
    }
}

/*
UsuarioController gerencia a lógica de negócios relacionada a usuários no sistema.
- Permite cadastrar usuários (manual ou automático), validar senhas e verificar duplicidade de e-mail.
- Atualiza e exclui usuários, garantindo consistência e integridade dos dados.
- Consulta usuários por empresa ou por ID.
- Utiliza HashService para gerar hash + salt das senhas, garantindo segurança.
- Retorna "ok"/"erro" ou mensagens detalhadas para a interface (View).
- Centraliza toda a lógica de autenticação, cadastro e manutenção de usuários, separando a lógica do banco de dados (DAO) da interface.
*/
