using MySql.Data.MySqlClient;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{

    public class EmpresaDAO
    {
        private string connectionString = "server=localhost;database=MiniTCC;uid=root;pwd=@260914Zveek;";

        public void Inserir(Empresa empresa)
        {
            // Usando 'using' para garantir que a conexão seja fechada corretamente
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Abrindo a conexão

                // Query SQL com colunas que existem na tabela 'empresa'
                string sql = @"INSERT INTO empresa 
                           (cnpj, nome_fantasia, email, telefone, razao_social, endereco) 
                           VALUES (@cnpj, @nome_fantasia, @email, @telefone, @razao_social, @endereco)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Passando os parâmetros da classe Empresa
                    cmd.Parameters.AddWithValue("@cnpj", empresa.CNPJ);
                    cmd.Parameters.AddWithValue("@nome_fantasia", empresa.Nome_fantasia);
                    cmd.Parameters.AddWithValue("@email", empresa.Email);
                    cmd.Parameters.AddWithValue("@telefone", empresa.Telefone);
                    cmd.Parameters.AddWithValue("@razao_social", empresa.Razao_social);
                    cmd.Parameters.AddWithValue("@endereco", empresa.Endereco);

                    // Executa o comando
                    cmd.ExecuteNonQuery();

                    // Pega o ID gerado pelo banco
                    empresa.Id = (int)cmd.LastInsertedId;
                }
            }
        }
    }
}
