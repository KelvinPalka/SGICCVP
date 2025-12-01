using MySql.Data.MySqlClient; // Importa classes para conectar e executar comandos no MySQL
using System.Collections.Generic; // Importa listas genéricas
using WPF_Projeto_BD.Models; // Importa os modelos (Cliente)

namespace WPF_Projeto_BD.Data.DAO // Namespace para a camada de acesso a dados
{
    public class ClienteDAO // Classe responsável por operações CRUD de Cliente no banco
    {
        // ==========================
        // Inserir um novo cliente
        // ==========================
        public void Inserir(Cliente cliente) // Método para inserir um novo cliente
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = @"INSERT INTO Cliente  
                               (nome, cpf_cnpj, endereco, telefone, email) 
                               VALUES (@nome, @cpf_cnpj, @endereco, @telefone, @email)"; // Comando SQL para inserir cliente
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL

                // Adiciona parâmetros ao comando
                cmd.Parameters.AddWithValue("@nome", cliente.Nome); // Nome do cliente
                cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ); // CPF/CNPJ do cliente
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco); // Endereço do cliente
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone); // Telefone do cliente
                cmd.Parameters.AddWithValue("@email", cliente.Email); // E-mail do cliente

                cmd.ExecuteNonQuery(); // Executa o comando (INSERT)
            }
        }

        // ==========================
        // Listar todos os clientes
        // ==========================
        public List<Cliente> Listar() // Método para listar todos os clientes
        {
            List<Cliente> lista = new List<Cliente>(); // Lista para armazenar os clientes

            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = "SELECT * FROM Cliente"; // Comando SQL para selecionar todos os clientes
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL

                var reader = cmd.ExecuteReader(); // Executa consulta
                while (reader.Read()) // Lê cada registro
                {
                    lista.Add(new Cliente // Adiciona cliente à lista
                    {
                        Id = reader.GetInt32("id_cliente"), // ID do cliente
                        Nome = reader.GetString("nome"), // Nome do cliente
                        CPF_CNPJ = reader.GetString("cpf_cnpj"), // CPF/CNPJ do cliente
                        Endereco = reader.GetString("endereco"), // Endereço do cliente
                        Telefone = reader.GetString("telefone"), // Telefone do cliente
                        Email = reader.GetString("email") // E-mail do cliente
                    });
                }
            }

            return lista; // Retorna a lista de clientes
        }

        // ==========================
        // Excluir cliente pelo ID
        // ==========================
        public void Excluir(int id) // Método para excluir um cliente pelo ID
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = "DELETE FROM Cliente WHERE id_cliente = @id_cliente"; // Comando SQL para excluir cliente
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@id_cliente", id); // Adiciona parâmetro ID
                cmd.ExecuteNonQuery(); // Executa o comando (DELETE)
            } 
        }

        // ==========================
        // Editar cliente existente
        // ==========================
        public void Editar(Cliente cliente) // Método para editar um cliente existente
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = @"UPDATE Cliente SET  
                               nome = @nome, 
                               cpf_cnpj = @cpf_cnpj, 
                               endereco = @endereco, 
                               telefone = @telefone, 
                               email = @email 
                               WHERE id_cliente = @id_cliente"; // Comando SQL para atualizar cliente
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@nome", cliente.Nome); // Nome do cliente
                cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ); // CPF/CNPJ do cliente
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco); // Endereço do cliente
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone); // Telefone do cliente
                cmd.Parameters.AddWithValue("@email", cliente.Email); // E-mail do cliente
                cmd.Parameters.AddWithValue("@id_cliente", cliente.Id); // ID do cliente
                cmd.ExecuteNonQuery(); // Executa o comando (UPDATE)
            }
        }

        // ==========================
        // Listar apenas nomes dos clientes
        // ==========================
        public List<Cliente> ListarNomes() // Método para listar apenas os nomes dos clientes
        {
            List<Cliente> lista = new List<Cliente>(); // Lista para armazenar os clientes

            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = "SELECT id_cliente, nome FROM Cliente ORDER BY nome"; // Comando SQL para selecionar IDs e nomes dos clientes
                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                 
                var reader = cmd.ExecuteReader(); // Executa consulta
                while (reader.Read()) // Lê cada registro
                {
                    lista.Add(new Cliente // Adiciona cliente à lista
                    {
                        Id = reader.GetInt32("id_cliente"), // ID do cliente
                        Nome = reader.GetString("nome") // Nome do cliente
                    }); 
                }
            }

            return lista; // Retorna a lista de clientes
        }

        // ==========================
        // Verifica duplicidade de CPF/CNPJ
        // ==========================
        public bool ExisteCPF_CNPJ(string cpf_cnpj, int? idIgnorar = null) // Método para verificar duplicidade de CPF/CNPJ
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = "SELECT COUNT(*) FROM Cliente WHERE cpf_cnpj = @cpf"; // Comando SQL para contar registros com o mesmo CPF/CNPJ
                if (idIgnorar.HasValue) // Se idIgnorar tiver valor
                    sql += " AND id_cliente != @id"; // Adiciona condição para ignorar o cliente atual

                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@cpf", cpf_cnpj); //  Adiciona parâmetro CPF/CNPJ
                if (idIgnorar.HasValue) // Se idIgnorar tiver valor
                    cmd.Parameters.AddWithValue("@id", idIgnorar.Value); // Adiciona parâmetro ID para ignorar

                long count = (long)cmd.ExecuteScalar(); // Executa consulta e obtém a contagem
                return count > 0; // Retorna true se já existir
            }
        }

        // ==========================
        // Verifica duplicidade de e-mail
        // ==========================
        public bool ExisteEmail(string email, int? idIgnorar = null) // Método para verificar duplicidade de e-mail
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                string sql = "SELECT COUNT(*) FROM Cliente WHERE email = @Email"; // Comando SQL para contar registros com o mesmo e-mail
                if (idIgnorar.HasValue) // Se idIgnorar tiver valor
                    sql += " AND id_cliente != @id"; // Adiciona condição para ignorar o cliente atual

                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@Email", email); // Adiciona parâmetro e-mail
                if (idIgnorar.HasValue) // Se idIgnorar tiver valor
                    cmd.Parameters.AddWithValue("@id", idIgnorar.Value); // Adiciona parâmetro ID para ignorar

                long count = (long)cmd.ExecuteScalar(); // Executa consulta e obtém a contagem
                return count > 0; // Retorna true se já existir
            }
        }
    }
}

/*
ClienteDAO gerencia a persistência de dados da entidade Cliente no banco MySQL.
- Permite inserir, listar, editar e excluir clientes.
- Fornece métodos especializados para listar apenas nomes ou verificar duplicidade de CPF/CNPJ e e-mail.
- Utiliza parâmetros SQL para evitar injeção de dados e garantir segurança.
- Centraliza toda a lógica de acesso a dados da entidade Cliente, separando a camada de negócios (Controller) da persistência.
*/
