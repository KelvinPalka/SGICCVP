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

        public Empresa ObterEmpresaPorId(int idEmpresa)
        {
            Empresa emp = null;

            using (var conn = Connection.GetConnection())
            {
                string sql = @"
                    SELECT * 
                    FROM empresa
                    WHERE id_empresa = @id";

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idEmpresa);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        emp = new Empresa
                        {
                            Id = dr.GetInt32("id_empresa"),
                            CNPJ = dr.GetString("cnpj"),
                            Nome_fantasia = dr.GetString("nome_fantasia"),
                            Razao_social = dr.GetString("razao_social"),
                            Email = dr.GetString("email"),
                            Telefone = dr.GetString("telefone"),
                            Endereco = dr.GetString("endereco")
                        };
                    }
                }
            }

            return emp;
        }

        public bool Editar(Empresa empresa)
        {
            try
            {
                using (var conn = Connection.GetConnection())
                {
                    var cmd = new MySqlCommand(@"
                UPDATE empresa
                SET cnpj = @CNPJ,
                    nome_fantasia = @NomeFantasia,
                    email = @Email,
                    telefone = @Telefone,
                    razao_social = @Razao,
                    endereco = @Endereco
                WHERE id_empresa = @Id;
            ", conn);
                    cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                    cmd.Parameters.AddWithValue("@NomeFantasia", empresa.Nome_fantasia);
                    cmd.Parameters.AddWithValue("@Email", empresa.Email);
                    cmd.Parameters.AddWithValue("@Telefone", empresa.Telefone);
                    cmd.Parameters.AddWithValue("@Razao", empresa.Razao_social);
                    cmd.Parameters.AddWithValue("@Endereco", empresa.Endereco);
                    cmd.Parameters.AddWithValue("@Id", empresa.Id);

                    cmd.ExecuteNonQuery();
                }

                return true; // se chegou aqui, deu certo
            }
            catch
            {
                return false; // se der erro, retorna false
            }
        }

        public bool ExisteCNPJ(string cnpj)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE cnpj = @cnpj";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cnpj", cnpj);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteEmail(string email)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteRazaoSocial(string razaoSocial)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE razao_social = @razaoSocial";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@razaoSocial", razaoSocial);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }


    }
}
