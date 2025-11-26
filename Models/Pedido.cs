using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projeto_BD.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data_pedido { get; set; }
        public DateTime? Data_entrega { get; set; } 
        public int Qntd { get; set; }
        public double Valor { get; set; }
        public string Status_pedido { get; set; }
        public string Descricao { get; set; }
        public int Id_cliente { get; set; }
        public int Id_produto { get; set; }
        

        public Pedido() { }

        public Pedido(int id, DateTime data_pedido, DateTime? data_entrega, int qntd, double valor, string status_pedido, string descricao, int id_cliente, int id_produto)
        {
            Id = id;
            Data_pedido = data_pedido;
            Data_entrega = data_entrega;
            Qntd = qntd;
            Valor = valor;
            Status_pedido = status_pedido;
            Descricao = descricao;
            Id_cliente = id_cliente;
            Id_produto = id_produto;
        }


    }
}
