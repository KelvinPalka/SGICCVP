using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Models;

namespace WPF_Projeto_BD.Data.DAO
{
    internal class PedidoDAO
    {
        public void Inserir()
        {
            // Implementar método para inserir pedido no banco de dados
        }

        public List<Pedido> Listar()
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (var conn = Connection.GetConnection())
            {
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

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    pedidos.Add(new Pedido
                    {
                        Id = reader.GetInt32("id_pedido"),
                        Data_pedido = reader.GetString("data_pedido"),
                        Data_entrega = reader.GetString("data_entrega"),
                        Qntd = reader.GetInt32("qntd"),
                        Valor = reader.GetDouble("valor"),
                        Status_pedido = reader.GetString("status_pedido"),
                        Descricao = reader.GetString("descricao"),
                        Id_cliente = reader.GetInt32("id_cliente"),
                        Id_produto = reader.GetInt32("id_produto")
                    });
                }
            }

            return pedidos;
        }
    }
}
