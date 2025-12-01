using System; // Importa classes básicas do .NET
using System.Collections.Generic; // Importa listas genéricas
using MySql.Data.MySqlClient; // Importa classes para conexão e comandos MySQL
using Wpf_Projeto_BD.Models; // Importa o modelo Pedido
using WPF_Projeto_BD.Models; // Caso tenha classes adicionais de modelos

namespace WPF_Projeto_BD.Data.DAO
{
    internal class PedidoDAO // Classe responsável por operações CRUD da entidade Pedido
    {
        // ==========================
        // Inserir um novo pedido
        // ==========================
        public void Inserir()
        {
            // Método ainda não implementado
            // Futuramente será usado para criar registros de pedidos no banco
        }

        // ==========================
        // Listar todos os pedidos
        // ==========================
        public List<Pedido> Listar()
        {
            List<Pedido> pedidos = new List<Pedido>(); // Cria lista para armazenar pedidos

            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                // Comando SQL para selecionar todos os pedidos
                string sql = @"SELECT 
                                id_pedido,
                                data_pedido,
                                data_entrega,
                                qntd,
                                valor,
                                status_pedido,
                                descricao,
                                id_cliente,
                                id_produto
                               FROM Pedido";

                MySqlCommand cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                var reader = cmd.ExecuteReader(); // Executa consulta e obtém leitor de dados

                while (reader.Read()) // Para cada registro retornado
                {
                    pedidos.Add(new Pedido
                    {
                        Id = reader.GetInt32("id_pedido"), // ID do pedido
                        Data_pedido = reader.GetDateTime("data_pedido"), // Data de criação do pedido
                        Data_entrega = reader.IsDBNull(reader.GetOrdinal("data_entrega")) // Verifica se a data de entrega é nula
                            ? (DateTime?)null
                            : reader.GetDateTime("data_entrega"),
                        Qntd = reader.GetInt32("qntd"), // Quantidade do pedido
                        Valor = reader.GetDouble("valor"), // Valor do pedido
                        Status_pedido = reader.GetString("status_pedido"), // Status (ex: "pendente", "entregue")
                        Descricao = reader.GetString("descricao"), // Descrição do pedido
                        Id_cliente = reader.GetInt32("id_cliente"), // ID do cliente que realizou o pedido
                        Id_produto = reader.GetInt32("id_produto") // ID do produto relacionado
                    });
                }
            }

            return pedidos; // Retorna a lista de pedidos
        }
    }
}

/*
PedidoDAO gerencia a persistência de dados da entidade Pedido no banco MySQL.
- Permite listar todos os pedidos cadastrados no sistema.
- Futuramente permitirá inserção, edição e exclusão de pedidos.
- Utiliza ExecuteReader para ler os registros e mapear para objetos Pedido.
- Centraliza a lógica de acesso a dados da entidade Pedido, separando a camada de negócios (Controller) da persistência.
*/
