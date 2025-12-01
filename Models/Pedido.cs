using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Pedido // Classe que representa um pedido realizado por um cliente no sistema
    {
        public int Id { get; set; } // Identificador único do pedido
        public DateTime Data_pedido { get; set; } // Data em que o pedido foi realizado
        public DateTime? Data_entrega { get; set; } // Data prevista ou realizada da entrega (nullable)
        public int Qntd { get; set; } // Quantidade de produtos solicitados no pedido
        public double Valor { get; set; } // Valor total do pedido
        public string Status_pedido { get; set; } // Status atual do pedido (ex: pendente, concluído)
        public string Descricao { get; set; } // Descrição detalhada do pedido
        public int Id_cliente { get; set; } // ID do cliente que realizou o pedido
        public int Id_produto { get; set; } // ID do produto solicitado no pedido

        // Construtor vazio
        public Pedido() { }

        // Construtor parametrizado para criar um pedido com todos os dados
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

/*
Resumo técnico:
- Pedido é um Model que representa os pedidos realizados por clientes no sistema.
- Contém informações essenciais como ID, datas de pedido e entrega, quantidade, valor, status, descrição, cliente e produto associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de pedidos, separando a lógica de negócios (Controller) e persistência (DAO).
*/
