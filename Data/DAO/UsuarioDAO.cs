using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario usuario)
        {
            using (var conn = Connection.GetConnection())
            {
                var cmd = new MySqlCommand(@"
                    INSERT INTO usuario
                    (id_empresa, nome, email, senha, tipo_usuario)
                    VALUES
                    (@IdEmp, @Nome, @Email, @Senha, @Tipo);
                ", conn);

                cmd.Parameters.AddWithValue("@IdEmp", usuario.IdEmpresa);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@Tipo", usuario.TipoUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        private readonly string conexao = "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        public Usuario Login(string email, string senha)
        {
            Usuario usuario = null;

            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT * FROM usuario WHERE email = @email AND senha = @senha";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha); // depois vamos criptografar

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        Id = dr.GetInt32("id_usuario"),
                        IdEmpresa = dr.GetInt32("id_empresa"),
                        Nome = dr.GetString("nome"),
                        Email = dr.GetString("email"),
                        Senha = dr.GetString("senha"),
                        TipoUsuario = dr.GetString("tipo_usuario")
                    };
                }
            }

            return usuario;
        }

        public List<Usuario> ObterTodos(int idEmpresa)
        {
            var lista = new List<Usuario>();

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM usuario WHERE id_empresa = @idEmpresa";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            Id = dr.GetInt32("id_usuario"),
                            Nome = dr.GetString("nome"),
                            Email = dr.GetString("email"),
                            Senha = dr.GetString("senha"),
                            TipoUsuario = dr.GetString("tipo_usuario"),
                            IdEmpresa = dr.GetInt32("id_empresa") // <-- aqui!
                        });
                    }
                }
            }

            return lista;
        }

        public bool Excluir(int idUsuario)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = "DELETE FROM usuario WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idUsuario);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool Editar(Usuario usuario)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = @"UPDATE usuario 
                               SET nome=@nome, email=@email, senha=@senha, tipoUsuario=@tipoUsuario 
                               WHERE id=@id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@tipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public Usuario ObterPorId(int idUsuario)
        {
            Usuario u = null;
            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM usuario WHERE id=@id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        u = new Usuario
                        {
                            Id = dr.GetInt32("id"),
                            Nome = dr.GetString("nome"),
                            Email = dr.GetString("email"),
                            Senha = dr.GetString("senha"),
                            TipoUsuario = dr.GetString("tipoUsuario"),
                            IdEmpresa = dr.GetInt32("idEmpresa")
                        };
                    }
                }
            }
            return u;
        }
    }
}
