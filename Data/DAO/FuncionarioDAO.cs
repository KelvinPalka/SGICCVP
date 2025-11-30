using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Wpf_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    internal class FuncionarioDAO
    {
        // ==========================
        // Inserir um novo funcionário
        // ==========================
        public void Inserir(Funcionario funcionario)
        {
            using (var conn = Connection.GetConnection())
            {
                string sql = @"
                    INSERT INTO funcionario 
                    (nome, cpf, cargo, telefone, email, Departamento, IdEmpresa)
                    VALUES (@nome, @cpf, @cargo, @telefone, @email, @departamento, @idEmpresa)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                cmd.Parameters.AddWithValue("@email", funcionario.Email);
                cmd.Parameters.AddWithValue("@departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@idEmpresa", funcionario.IdEmpresa);

                cmd.ExecuteNonQuery();
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

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
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
                        lista.Add(f);
                    }
                }
            }

            return lista;
        }

        // ==========================
        // Atualizar funcionário
        // ==========================
        public bool Editar(Funcionario funcionario)
        {
            try
            {
                using (var conn = Connection.GetConnection())
                {
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

                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                    cmd.Parameters.AddWithValue("@email", funcionario.Email);
                    cmd.Parameters.AddWithValue("@departamento", funcionario.Departamento);
                    cmd.Parameters.AddWithValue("@id", funcionario.Id);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
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
                    string sql = "DELETE FROM funcionario WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idFuncionario);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ==========================
        // Obter funcionário por ID
        // ==========================
        public Funcionario ObterPorId(int idFuncionario)
        {
            Funcionario funcionario = null;

            using (var conn = Connection.GetConnection())
            {
                string sql = "SELECT * FROM funcionario WHERE id_funcionario = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idFuncionario);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
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

            return funcionario;
        }
    }
}
