using MySql.Data.MySqlClient; // Importa o namespace do MySQL
using System.Collections.Generic; // Importa o namespace para listas genéricas
using WPF_Projeto_BD.Models; // Importa o namespace dos modelos (Models)

namespace WPF_Projeto_BD.Data.DAO // Define o namespace da aplicação (Data/DAO)
{
    public class ClienteDAO // Define a classe ClienteDAO
    {
        public void Inserir(Cliente cliente) // Método para inserir um cliente no banco de dados
        {
            using (var conn = Connection.GetConnection()) // Obtém a conexão com o banco de dados
            {
                string sql = @"INSERT INTO Cliente  
                           (nome, cpf_cnpj, endereco, telefone, email) 
                           VALUES (@nome, @cpf_cnpj, @endereco, @telefone, @email)"; // Define a consulta SQL para inserção

                MySqlCommand cmd = new MySqlCommand(sql, conn);     // Cria o comando SQL

                // Adiciona os parâmetros ao comando
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@email", cliente.Email);

                // Executa o comando SQL
                cmd.ExecuteNonQuery();
            }
        }


        public List<Cliente> Listar() // Método para listar todos os clientes do banco de dados
        {
            List<Cliente> lista = new List<Cliente>(); // Cria uma lista para armazenar os clientes

            using (var conn = Connection.GetConnection()) // Obtém a conexão com o banco de dados
            {
                string sql = "SELECT * FROM Cliente"; // Define a consulta SQL para seleção
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria o comando SQL

                var reader = cmd.ExecuteReader(); // Executa o comando SQL e obtém o leitor de dados
                while (reader.Read()) // Lê os dados retornados
                {
                    lista.Add(new Cliente // Adiciona um novo cliente à lista
                    {
                        Id = reader.GetInt32("id_cliente"),
                        Nome = reader.GetString("nome"),
                        CPF_CNPJ = reader.GetString("cpf_cnpj"),
                        Endereco = reader.GetString("endereco"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email")
                    });
                }
            }

            return lista;
        }
        public void Excluir(int id) // Método para excluir um cliente do banco de dados
        {
            // Exclui o cliente com o ID especificado
            using (var conn = Connection.GetConnection())
            {
                string sql = "DELETE FROM Cliente WHERE id_cliente = @id_cliente";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id_cliente", id); // Adiciona o parâmetro ao comando
                cmd.ExecuteNonQuery(); // Executa o comando SQL
            }
        }

        public void Editar(Cliente cliente) // Método para editar um cliente no banco de dados
        {
            // Atualiza os dados do cliente com o ID especificado
            using (var conn = Connection.GetConnection())
            {
                string sql = @"UPDATE Cliente SET 
                               nome = @nome, 
                               cpf_cnpj = @cpf_cnpj, 
                               endereco = @endereco, 
                               telefone = @telefone, 
                               email = @email 
                               WHERE id_cliente = @id_cliente";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.Parameters.AddWithValue("@id_cliente", cliente.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Cliente> ListarNomes() // Método para listar apenas os nomes dos clientes
        {
            List<Cliente> lista = new List<Cliente>(); // Cria uma lista para armazenar os clientes

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT id_cliente, nome FROM Cliente ORDER BY nome"; 
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader(); // Executa o comando SQL e obtém o leitor de dados

                while (reader.Read()) // Lê os dados retornados
                {
                    lista.Add(new Cliente
                    {
                        Id = reader.GetInt32("id_cliente"),
                        Nome = reader.GetString("nome")
                    });
                }
            }

            return lista;
        }


    }
}