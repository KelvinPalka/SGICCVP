using System.Collections.Generic;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Controllers
{
    public class ClienteController
    {
        private ClienteDAO dao = new ClienteDAO();

        public void SalvarCliente(Cliente c)
        {
            dao.Inserir(c);
        }

        public List<Cliente> GetClientes()
        {
            return dao.Listar();
        }

        public void EditarCliente(Cliente cliente)
        {
            dao.Editar(cliente);
        }

        public void ExcluirCliente(int id)
        {
            dao.Excluir(id);
        }

        public List<Cliente> GetClientesSimples()
        {
            return dao.ListarNomes();
        }

    }
}
