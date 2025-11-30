using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Utils; // HashService

namespace WPF_Projeto_BD.Data.DAO
{
    public class UsuarioDAO
    {
        private readonly string conexao =
            "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        // -------------------------------
        //  INSERIR (gera hash + salt)
        // -------------------------------
        public void Inserir(Usuario usuario)
        {
            var resultado = HashService.GerarHashComSalt(usuario.SenhaHash);

            usuario.SenhaHash = resultado.hash;
            usuario.Salt = resultado.salt;

            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                var cmd = new MySqlCommand(@"
                    INSERT INTO usuario
                    (id_empresa, nome, email, senha_hash, salt, tipo_usuario)
                    VALUES
                    (@IdEmpresa, @Nome, @Email, @SenhaHash, @Salt, @TipoUsuario);
                ", conn);

                cmd.Parameters.AddWithValue("@IdEmpresa", usuario.IdEmpresa);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@Salt", usuario.Salt);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        // -------------------------------
        //  LOGIN (verifica hash + salt)
        // -------------------------------
        public Usuario Login(string email, string senhaDigitada)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "SELECT * FROM usuario WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return null; // email não existe

                    string hashBanco = dr.GetString("senha_hash");
                    string saltBanco = dr.GetString("salt");

                    if (!HashService.VerificarHashComSalt(senhaDigitada, hashBanco, saltBanco))
                        return null; // senha incorreta

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

        // -------------------------------
        //  OBTER TODOS
        // -------------------------------
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
                    while (dr.Read())
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

            return lista;
        }

        // -------------------------------
        //  EXCLUIR
        // -------------------------------
        public bool Excluir(int idUsuario)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM usuario WHERE id_usuario = @Id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idUsuario);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------------
        //  EDITAR (gera novo hash + salt)
        // -------------------------------
        public bool Atualizar(Usuario usuario)
        {
            var resultado = HashService.GerarHashComSalt(usuario.SenhaHash);

            usuario.SenhaHash = resultado.hash;
            usuario.Salt = resultado.salt;

            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = @"
                    UPDATE usuario SET 
                        nome = @Nome,
                        email = @Email,
                        senha_hash = @SenhaHash,
                        salt = @Salt,
                        tipo_usuario = @TipoUsuario
                    WHERE id_usuario = @IdUsuario";

                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@Salt", usuario.Salt);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------------
        //  OBTER POR ID
        // -------------------------------
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
                    if (dr.Read())
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

            return null;
        }

        // -------------------------------
        //  VERIFICAR DUPLICIDADE DE E-MAIL
        // -------------------------------
        public bool ExisteEmail(string email)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM usuario WHERE email = @Email";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

    }
}
