using MySql.Data.MySqlClient;
using WPF_Projeto_BD.Models;


namespace WPF_Projeto_BD.Data.DAO
{
    public class UsuarioDAO
    {
        private string connectionString = "server=localhost;database=MiniTCC;uid=root;pwd=@260914Zveek;";

        public void Inserir(Usuario usuario)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = @"INSERT INTO Usuario 
                       (nome, email, senha, tipo_usuario) 
                       VALUES (@nome, @email, @senha, @tipo_usuario)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@tipo_usuario", usuario.TipoUsuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
