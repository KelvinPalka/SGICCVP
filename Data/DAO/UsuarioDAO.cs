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
    }
}
