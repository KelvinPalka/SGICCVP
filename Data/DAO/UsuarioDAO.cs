using System; // Importa classes básicas do .NET
using System.Collections.Generic; // Importa listas genéricas
using MySql.Data.MySqlClient; // Importa classes para conexão e comandos MySQL
using WPF_Projeto_BD.Models; // Importa o modelo Usuario
using WPF_Projeto_BD.Utils; // Importa utilitário HashService para gerenciamento seguro de senhas

namespace WPF_Projeto_BD.Data.DAO
{
    public class UsuarioDAO // Classe responsável pelo CRUD e autenticação de usuários
    {
        private readonly string conexao =
            "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";
        // String de conexão centralizada

        // ==========================
        // Inserir usuário (hash + salt)
        // ==========================
        public void Inserir(Usuario usuario)
        {
            // Gera hash e salt a partir da senha do usuário
            var resultado = HashService.GerarHashComSalt(usuario.SenhaHash);
            usuario.SenhaHash = resultado.hash;
            usuario.Salt = resultado.salt;

            using (var conn = new MySqlConnection(conexao)) // Abre conexão
            {
                conn.Open(); // Abre a conexão antes de executar comandos

                // Comando SQL para inserir usuário
                var cmd = new MySqlCommand(@"
                    INSERT INTO usuario
                    (id_empresa, nome, email, senha_hash, salt, tipo_usuario)
                    VALUES
                    (@IdEmpresa, @Nome, @Email, @SenhaHash, @Salt, @TipoUsuario);
                ", conn);

                // Adiciona parâmetros ao comando SQL
                cmd.Parameters.AddWithValue("@IdEmpresa", usuario.IdEmpresa);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@Salt", usuario.Salt);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);

                cmd.ExecuteNonQuery(); // Executa o comando INSERT
            }
        }

        // ==========================
        // Login de usuário (verifica hash + salt)
        // ==========================
        public Usuario Login(string email, string senhaDigitada)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                // Consulta o usuário pelo e-mail
                string sql = "SELECT * FROM usuario WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return null; // Email não existe

                    // Recupera hash e salt do banco
                    string hashBanco = dr.GetString("senha_hash");
                    string saltBanco = dr.GetString("salt");

                    // Verifica se a senha digitada corresponde ao hash
                    if (!HashService.VerificarHashComSalt(senhaDigitada, hashBanco, saltBanco))
                        return null; // Senha incorreta

                    // Retorna objeto Usuario preenchido
                    return new Usuario
                    {
                        IdUsuario = dr.GetInt32("id_usuario"),
                        IdEmpresa = dr.GetInt32("id_empresa"),
                        Nome = dr.GetString("nome"),
                        Email = dr.GetString("email"),
                        SenhaHash = hashBanco,
                        Salt = saltBanco,
                        TipoUsuario = dr.GetString("tipo_usuario")
                    };
                }
            }
        }

        // ==========================
        // Obter todos os usuários de uma empresa
        // ==========================
        public List<Usuario> ObterTodos(int idEmpresa)
        {
            var lista = new List<Usuario>();

            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT * FROM usuario WHERE id_empresa = @IdEmpresa";

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) // Para cada registro
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = dr.GetInt32("id_usuario"),
                            Nome = dr.GetString("nome"),
                            Email = dr.GetString("email"),
                            SenhaHash = dr.GetString("senha_hash"),
                            Salt = dr.GetString("salt"),
                            TipoUsuario = dr.GetString("tipo_usuario"),
                            IdEmpresa = dr.GetInt32("id_empresa")
                        });
                    }
                }
            }

            return lista; // Retorna todos os usuários da empresa
        }

        // ==========================
        // Excluir usuário
        // ==========================
        public bool Excluir(int idUsuario)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM usuario WHERE id_usuario = @Id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idUsuario);

                return cmd.ExecuteNonQuery() > 0; // Retorna true se algum registro foi excluído
            }
        }

        // ==========================
        // Atualizar usuário (novo hash + salt)
        // ==========================
        public bool Atualizar(Usuario usuario)
        {
            // Gera novo hash e salt da senha
            var resultado = HashService.GerarHashComSalt(usuario.SenhaHash);
            usuario.SenhaHash = resultado.hash;
            usuario.Salt = resultado.salt;

            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                // Comando SQL para atualizar usuário
                string sql = @"
                    UPDATE usuario SET 
                        nome = @Nome,
                        email = @Email,
                        senha_hash = @SenhaHash,
                        salt = @Salt,
                        tipo_usuario = @TipoUsuario
                    WHERE id_usuario = @IdUsuario";

                var cmd = new MySqlCommand(sql, conn);

                // Adiciona parâmetros
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@Salt", usuario.Salt);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                return cmd.ExecuteNonQuery() > 0; // Retorna true se atualização ocorreu
            }
        }

        // ==========================
        // Obter usuário por ID
        // ==========================
        public Usuario ObterPorId(int idUsuario)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "SELECT * FROM usuario WHERE id_usuario = @Id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idUsuario);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) // Se registro encontrado
                    {
                        return new Usuario
                        {
                            IdUsuario = dr.GetInt32("id_usuario"),
                            Nome = dr.GetString("nome"),
                            Email = dr.GetString("email"),
                            SenhaHash = dr.GetString("senha_hash"),
                            Salt = dr.GetString("salt"),
                            TipoUsuario = dr.GetString("tipo_usuario"),
                            IdEmpresa = dr.GetInt32("id_empresa")
                        };
                    }
                }
            }

            return null; // Retorna null se usuário não encontrado
        }

        // ==========================
        // Verificar duplicidade de e-mail
        // ==========================
        public bool ExisteEmail(string email)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM usuario WHERE email = @Email";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                long count = (long)cmd.ExecuteScalar(); // Conta registros com mesmo e-mail
                return count > 0; // Retorna true se já existir
            }
        }
    }
}

/*
UsuarioDAO gerencia a persistência e autenticação de usuários no banco MySQL.
- Permite inserir usuários com hash + salt, autenticar login, listar, atualizar e excluir usuários.
- Inclui métodos para verificar duplicidade de e-mail.
- Utiliza ExecuteReader para consultas e ExecuteNonQuery para alterações.
- Centraliza lógica de acesso a dados, separando a camada de negócios (Controller) da persistência.
- Garante segurança de senhas usando HashService.
*/
