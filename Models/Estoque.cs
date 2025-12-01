using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Estoque // Classe que representa um registro de estoque no sistema
    {
        public int Id { get; set; } // Identificador único do registro de estoque
        public string Data { get; set; } // Data em que o estoque foi registrado ou atualizado
        public string Local_armazenamento { get; set; } // Local onde os produtos estão armazenados

        // Construtor vazio
        public Estoque() { }

        // Construtor parametrizado para criar um registro de estoque com todos os dados
        public Estoque(int id, string data, string local_armazenamento)
        {
            Id = id;
            Data = data;
            Local_armazenamento = local_armazenamento;
        }
    }
}

/*
Resumo técnico:
- Estoque é um Model que representa os registros de armazenamento de produtos no sistema.
- Contém informações essenciais como ID, data do registro e local de armazenamento.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de estoque, separando a lógica de negócios (Controller) e persistência (DAO).
*/
