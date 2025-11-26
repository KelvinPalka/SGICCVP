using System.Collections.Generic;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Controllers
{
    public class PedidoController
    {
        private PedidoDAO dao = new PedidoDAO();

        // Retorna todos os pedidos cadastrados
        public List<Pedido> GetPedidos()
        {
            return dao.Listar();
        }
    }
}
