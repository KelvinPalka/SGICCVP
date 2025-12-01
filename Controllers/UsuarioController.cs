using System; // Importa classes básicas do .NET
using System.Collections.Generic; // Importa listas genéricas
using System.Linq; // Importa LINQ para consultas em coleções
using WPF_Projeto_BD.Data.DAO; // Importa os DAOs para acesso ao banco
using WPF_Projeto_BD.Utils; // Importa utilitários (HashService)

namespace WPF_Projeto_BD.Controllers // Define o namespace dos controllers
{
    internal class UsuarioController // Controller responsável pela lógica de negócios de usuários
    {
        private readonly UsuarioDAO usuarioDao = new UsuarioDAO(); // DAO para operações de usuários

        // =========================
        // Cadastrar usuário (manual)
        // =========================
        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa) // Método para cadastrar um novo usuário
        {
            return CadastrarUsuario(nome, email, senha, tipoUsuario, idEmpresa, false);
            // Chama método sobrecarregado permitindo apenas senhas normais (não numéricas)
        }

        // =========================
        // Cadastrar usuário com opção de senha apenas numérica (automático)
        // =========================
        public string CadastrarUsuario(string nome, string email, string senha, string tipoUsuario, int idEmpresa, bool permitirApenasNumeros) // Método para cadastrar um novo usuário
        {
            // 1) Validar campos obrigatórios
            if (string.IsNullOrWhiteSpace(nome)) // Valida nome não vazio
                return "O nome é obrigatório."; //  Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) // Valida e-mail básico
                return "E-mail inválido."; // Retorna mensagem de erro

            if (!SenhaValida(senha, permitirApenasNumeros)) // Valida senha conforme regras
                return permitirApenasNumeros 
                    ? "Senha inválida." // Retorna mensagem de erro para senhas numéricas
                    : "A senha deve ter pelo menos 6 caracteres, incluindo letras e números."; // Retorna mensagem de erro para senhas normais

            // 2) Verificar duplicidade de e-mail
            if (usuarioDao.ExisteEmail(email)) // Verifica se e-mail já existe
                return "Este e-mail já está cadastrado."; // Retorna mensagem de erro

            // 3) Gerar hash + salt da senha
            var (hash, salt) = HashService.GerarHashComSalt(senha); // Gera hash e salt seguros 

            var usuario = new Usuario // Cria novo objeto Usuario
            {
                Nome = nome, // Define nome
                Email = email, // Define e-mail
                SenhaHash = hash, // Define hash da senha
                Salt = salt, // Define salt da senha
                TipoUsuario = tipoUsuario, // Define tipo de usuário
                IdEmpresa = idEmpresa // Define ID da empresa
            };

            // 4) Inserir no banco
            usuarioDao.Inserir(usuario); // Insere usuário no banco

            return "ok"; // Retorna "ok" se cadastro for bem-sucedido
        }

        // =========================
        // Obter todos os usuários de uma empresa
        // =========================
        public List<Usuario> ObterTodos(int idEmpresa) // Método para obter todos os usuários de uma empresa
        {
            return usuarioDao.ObterTodos(idEmpresa); // Retorna lista de usuários
        }

        // =========================
        // Atualizar usuário existente
        // =========================
        public string AtualizarUsuario(Usuario usuario, string novaSenha) // Método para atualizar um usuário existente
        {
            // Valida campos obrigatórios
            if (string.IsNullOrWhiteSpace(usuario.Nome)) // Valida nome não vazio
                return "O nome é obrigatório."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@")) // Valida e-mail básico
                return "E-mail inválido."; // Retorna mensagem de erro

            if (string.IsNullOrWhiteSpace(usuario.TipoUsuario) ||
                (usuario.TipoUsuario != "admin" && usuario.TipoUsuario != "user")) // Valida tipo de usuário
                return "O tipo de conta deve ser 'admin' ou 'user'."; // Retorna mensagem de erro

            // ================= VALIDAÇÃO DE SENHA =================
            if (string.IsNullOrWhiteSpace(novaSenha)) // Sem nova senha informada
            {
                if (string.IsNullOrWhiteSpace(usuario.SenhaHash)) // Senha antiga vazia
                    return "A senha não pode ficar vazia."; // Senha antiga permanece se preenchida
            }
            else // Nova senha informada
            {
                if (!SenhaValida(novaSenha, false)) // Valida nova senha (não permite apenas números)
                    return "A senha deve ter pelo menos 6 caracteres, com letras e números."; // Retorna mensagem de erro

                // Atualiza hash e salt
                var resultado = HashService.GerarHashComSalt(novaSenha); // Gera novo hash e salt
                usuario.SenhaHash = resultado.hash; // Atualiza hash
                usuario.Salt = resultado.salt; // Atualiza salt
            }

            // Verifica duplicidade de e-mail entre outros usuários
            var todosUsuarios = usuarioDao.ObterTodos(usuario.IdEmpresa); // Obtém todos os usuários da empresa
            foreach (var u in todosUsuarios) // Percorre cada usuário
            {
                if (u.Email == usuario.Email && u.IdUsuario != usuario.IdUsuario) // E-mail já usado por outro usuário
                    return "Este e-mail já está cadastrado por outro usuário."; // Retorna mensagem de erro
            }

            bool sucesso = usuarioDao.Atualizar(usuario); // Atualiza usuário no banco
            return sucesso ? "ok" : "erro"; // Operador ternário: retorna "ok" se true, "erro" se false
        }

        // =========================
        // Excluir usuário pelo ID
        // =========================
        public bool ExcluirUsuario(int idUsuario) // Método para excluir um usuário pelo ID
        {
            return usuarioDao.Excluir(idUsuario); // Remove usuário do banco
        }

        // =========================
        // Obter usuário por ID
        // =========================
        public Usuario ObterPorId(int idUsuario) // Método para obter um usuário pelo ID
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
        private bool SenhaValida(string senha, bool permitirApenasNumeros) // Método para validar a senha conforme regras
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6) // Valida mínimo 6 caracteres
                return false; // Retorna false se inválida

            if (permitirApenasNumeros) // Se permitido apenas números
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
