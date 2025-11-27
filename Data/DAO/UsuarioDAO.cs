using System.Data.SqlClient;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario usuario)
        {
            using (var conn = Connection.GetConnection())
            {
                var cmd = new SqlCommand(
                @"INSERT INTO Usuarios 
                (Nome, Email, Senha, IdEmpresa)
                VALUES (@Nome, @Email, @Senha, @IdEmp);");

                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@IdEmp", usuario.IdEmpresa);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
