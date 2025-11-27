using System;
using MySql.Data.MySqlClient;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    public class EmpresaDAO
    {
        public int Inserir(Empresa empresa)
        {
            using (var conn = Connection.GetConnection())
            {
                var cmd = new MySqlCommand(@"
                    INSERT INTO empresa 
                    (cnpj, nome_fantasia, email, telefone, razao_social, endereco)
                    VALUES
                    (@CNPJ, @NomeFantasia, @Email, @Telefone, @Razao, @Endereco);

                    SELECT LAST_INSERT_ID();
                ", conn);

                cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                cmd.Parameters.AddWithValue("@NomeFantasia", empresa.Nome_fantasia);
                cmd.Parameters.AddWithValue("@Email", empresa.Email);
                cmd.Parameters.AddWithValue("@Telefone", empresa.Telefone);
                cmd.Parameters.AddWithValue("@Razao", empresa.Razao_social);
                cmd.Parameters.AddWithValue("@Endereco", empresa.Endereco);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
