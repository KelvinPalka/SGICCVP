using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Compra // Classe que representa uma compra realizada por um cliente
    {
        public int Id { get; set; } // Propriedade para armazenar o ID da compra (chave primária)
        public int Qntd { get; set; } // Quantidade de itens comprados
        public double Valor { get; set; } // Valor total da compra
        public int Id_cliente { get; set; } // ID do cliente associado à compra

        // Construtor vazio necessário para DAOs
        public Compra() { }

        // Construtor parametrizado para criar instâncias com todos os dados
        public Compra(int id, int qntd, double valor, int id_cliente)
        {
            Id = id; // Inicializa a propriedade Id
            Qntd = qntd; // Inicializa a propriedade Qntd
            Valor = valor; // Inicializa a propriedade Valor
            Id_cliente = id_cliente; // Inicializa a propriedade Id_cliente
        }
    }
}

/*
Resumo técnico:
- Compra é uma classe de entidade (Model) que representa compras realizadas por clientes.
- Contém propriedades para ID da compra, quantidade de itens, valor total e referência ao cliente (Id_cliente).
- Possui construtor vazio para compatibilidade com DAOs ou ORM.
- Possui construtor parametrizado para criação de objetos completos.
- Centraliza a representação de dados da compra, separando a lógica de negócios (Controller) e persistência (DAO).
*/
