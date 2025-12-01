using System; // Importa classes básicas do .NET
using System.Collections.Generic; // Importa listas genéricas
using MySql.Data.MySqlClient; // Importa classes para conectar e executar comandos no MySQL
using Wpf_Projeto_BD.Models; // Importa os modelos (Funcionario)

namespace WPF_Projeto_BD.Data.DAO
{
    internal class FuncionarioDAO // Classe responsável por operações CRUD da entidade Funcionario
    {
        // ==========================
        // Inserir um novo funcionário
        // ==========================
        public void Inserir(Funcionario funcionario)
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco de dados
            {
                // Comando SQL para inserir um funcionário
                string sql = @"
                    INSERT INTO funcionario 
                    (nome, cpf, cargo, telefone, email, Departamento, IdEmpresa)
                    VALUES (@nome, @cpf, @cargo, @telefone, @email, @departamento, @idEmpresa)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                // Adiciona os parâmetros ao comando SQL
                cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                cmd.Parameters.AddWithValue("@email", funcionario.Email);
                cmd.Parameters.AddWithValue("@departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@idEmpresa", funcionario.IdEmpresa);

                cmd.ExecuteNonQuery(); // Executa o comando INSERT
            }
        }

        // ==========================
        // Obter todos os funcionários de uma empresa
        // ==========================
        public List<Funcionario> ObterTodos(int id_empresa)
        {
            var lista = new List<Funcionario>();

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM funcionario WHERE IdEmpresa = @idEmpresa";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idEmpresa", id_empresa);

                using (var dr = cmd.ExecuteReader()) // Executa consulta e lê registros
                {
                    while (dr.Read()) // Para cada registro encontrado
                    {
                        var f = new Funcionario
                        {
                            Id = dr.GetInt32("id_funcionario"),
                            Nome = dr.GetString("nome"),
                            CPF = dr.GetString("cpf"),
                            Cargo = dr.GetString("cargo"),
                            Telefone = dr.GetString("telefone"),
                            Email = dr.GetString("email"),
                            Departamento = dr.GetString("Departamento"),
                            IdEmpresa = dr.GetInt32("Idempresa")
                        };
                        lista.Add(f); // Adiciona o funcionário à lista
                    }
                }
            }

            return lista; // Retorna todos os funcionários da empresa
        }

        // ==========================
        // Atualizar funcionário
        // ==========================
        public bool Atualizar(Funcionario funcionario)
        {
            try
            {
                using (var conn = Connection.GetConnection())
                {
                    // Comando SQL para atualizar dados do funcionário
                    string sql = @"
                        UPDATE funcionario
                        SET nome = @nome,
                            cpf = @cpf,
                            cargo = @cargo,
                            telefone = @telefone,
                            email = @email,
                            Departamento = @departamento
                        WHERE id_funcionario = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Adiciona os parâmetros
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                    cmd.Parameters.AddWithValue("@email", funcionario.Email);
                    cmd.Parameters.AddWithValue("@departamento", funcionario.Departamento);
                    cmd.Parameters.AddWithValue("@id", funcionario.Id);

                    cmd.ExecuteNonQuery(); // Executa o comando UPDATE
                }

                return true; // Retorna true se atualização foi bem-sucedida
            }
            catch
            {
                return false; // Retorna false em caso de erro
            }
        }

        // ==========================
        // Excluir funcionário
        // ==========================
        public bool Excluir(int idFuncionario)
        {
            try
            {
                using (var conn = Connection.GetConnection())
                {
                    string sql = "DELETE FROM funcionario WHERE id_funcionario = @id"; // SQL para deletar funcionário pelo ID
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idFuncionario);
                    cmd.ExecuteNonQuery(); // Executa comando DELETE
                }

                return true; // Retorna true se exclusão ocorreu com sucesso
            }
            catch
            {
                return false; // Retorna false em caso de erro
            }
        }

        // ==========================
        // Obter funcionário por ID
        // ==========================
        public Funcionario ObterPorId(int idFuncionario)
        {
            Funcionario funcionario = null; // Inicializa variável de retorno

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM funcionario WHERE id_funcionario = @id"; // SQL para consultar pelo ID
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idFuncionario);

                using (var dr = cmd.ExecuteReader()) // Executa consulta
                {
                    if (dr.Read()) // Se encontrar registro
                    {
                        funcionario = new Funcionario
                        {
                            Id = dr.GetInt32("id_funcionario"),
                            Nome = dr.GetString("nome"),
                            CPF = dr.GetString("cpf"),
                            Cargo = dr.GetString("cargo"),
                            Telefone = dr.GetString("telefone"),
                            Email = dr.GetString("email"),
                            Departamento = dr.GetString("Departamento"),
                            IdEmpresa = dr.GetInt32("IdEmpresa")
                        };
                    }
                }
            }

            return funcionario; // Retorna o funcionário ou null se não encontrado
        }
    }
}

/*
FuncionarioDAO gerencia a persistência de dados da entidade Funcionario no banco MySQL.
- Permite inserir, listar, atualizar, excluir e consultar funcionários por ID ou empresa.
- Utiliza parâmetros SQL para garantir segurança e evitar injeção de dados.
- Centraliza toda a lógica de acesso a dados da entidade Funcionario, separando a camada de negócios (Controller) da persistência.
*/
