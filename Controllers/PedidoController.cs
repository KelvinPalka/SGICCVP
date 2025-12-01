using System.Collections.Generic; // Importa listas genéricas
using WPF_Projeto_BD.Models; // Importa os modelos (Pedido)
using WPF_Projeto_BD.Data.DAO; // Importa os DAOs para acesso ao banco

namespace WPF_Projeto_BD.Controllers
{
    public class PedidoController // Controller responsável pela lógica de negócios de Pedidos
    {
        private PedidoDAO dao = new PedidoDAO(); // DAO para operações com pedidos

        // Retorna todos os pedidos cadastrados no banco
        public List<Pedido> GetPedidos()
        {
            return dao.Listar(); // Chama o DAO para listar todos os pedidos
        }
    }
}

/*
PedidoController gerencia a lógica de negócios relacionada a pedidos.
- Utiliza PedidoDAO para consultar os pedidos armazenados no banco de dados.
- Fornece método para obter todos os pedidos cadastrados, retornando uma lista de objetos Pedido.
- Centraliza a lógica de leitura de pedidos, separando o acesso aos dados da interface (View).
*/
