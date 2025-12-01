using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Entrega // Classe que representa uma entrega no sistema
    {
        public int Id { get; set; } // Identificador único da entrega
        public string Data_Entrega { get; set; } // Data em que a entrega foi realizada ou está prevista
        public string Status_entrega { get; set; } // Status atual da entrega (ex: pendente, em trânsito, concluída)
        public int Id_transportadora { get; set; } // ID da transportadora responsável pela entrega
        public int Id_pedido { get; private set; } // ID do pedido associado à entrega (somente leitura fora da classe)

        // Construtor vazio
        public Entrega() { }

        // Construtor parametrizado para criar uma entrega com todos os dados
        public Entrega(int id, string data_Entrega, string status_entrega, int id_transportadora, int id_pedido)
        {
            Id = id;
            Data_Entrega = data_Entrega;
            Status_entrega = status_entrega;
            Id_transportadora = id_transportadora;
            Id_pedido = id_pedido;
        }
    }
}

/*
Resumo técnico:
- Entrega é um Model que representa o registro de entregas de pedidos no sistema.
- Contém informações essenciais como ID, data da entrega, status, transportadora e pedido associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de entregas, separando a lógica de negócios (Controller) e persistência (DAO).
*/
