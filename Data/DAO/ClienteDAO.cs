using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Views;
namespace WPF_Projeto_BD.Data.DAO
{
    public class ClienteDAO
    {
        public void Inserir(Cliente cliente)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = @"INSERT INTO Cliente 
                           (nome, cpf_cnpj, endereco, telefone, email) 
                           VALUES (@nome, @cpf_cnpj, @endereco, @telefone, @email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@email", cliente.Email);

                cmd.ExecuteNonQuery();
            }
        }


        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM Cliente";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cliente
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
        public void Excluir(int id)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = "DELETE FROM Cliente WHERE id_cliente = @id_cliente";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id_cliente", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Cliente cliente)
        {
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

        public List<Cliente> ListarNomes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT id_cliente, nome FROM Cliente ORDER BY nome";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
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