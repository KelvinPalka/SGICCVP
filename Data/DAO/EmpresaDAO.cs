using MySql.Data.MySqlClient;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    public class EmpresaDAO
    {
        public void Inserir(Empresa empresa)
        {
            using (var conn = Connection.GetConnection())
            {
                var cmd = new MySqlCommand(
                @"INSERT INTO Empresas 
                (CNPJ, NomeFantasia, Email, Telefone, RazaoSocial, Endereco)
                VALUES (@CNPJ, @Nome, @Email, @Tel, @Razao, @End);", conn);

                cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                cmd.Parameters.AddWithValue("@Nome", empresa.Nome_fantasia);
                cmd.Parameters.AddWithValue("@Email", empresa.Email);
                cmd.Parameters.AddWithValue("@Tel", empresa.Telefone);
                cmd.Parameters.AddWithValue("@Razao", empresa.Razao_social);
                cmd.Parameters.AddWithValue("@End", empresa.Endereco);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
