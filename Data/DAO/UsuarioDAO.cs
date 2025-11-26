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

                string SQL = @"INSERT INTO usuario 
                       (nome, email, senha, tipo_usuario, id_empresa) 
                       VALUES (@nome, @email, @senha, @tipo_usuario, @id_empresa)";

                using (var cmd = new MySqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@tipo_usuario", usuario.TipoUsuario);
                    cmd.Parameters.AddWithValue("@id_empresa", usuario.IdEmpresa); // obrigatório

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}