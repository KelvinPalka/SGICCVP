using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Linha // Classe que representa um tipo de linha no sistema de estoque
    {
        public int Id { get; set; } // Identificador único da linha
        public string Cor { get; set; } // Cor da linha
        public double Qntd_estocada { get; set; } // Quantidade da linha atualmente estocada
        public double Valor { get; set; } // Valor unitário ou total da linha
        public int Id_estoque { get; } // ID do estoque ao qual a linha está vinculada (somente leitura)

        // Construtor vazio
        public Linha() { }

        // Construtor parametrizado para criar uma linha com todos os dados
        public Linha(int id, string cor, double qntd_estocada, double valor, int id_estoque)
        {
            Id = id;
            Cor = cor;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Id_estoque = id_estoque;
        }
    }
}

/*
Resumo técnico:
- Linha é um Model que representa os tipos de linhas armazenadas no estoque do sistema.
- Contém informações essenciais como ID, cor, quantidade estocada, valor e estoque associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de linhas, separando a lógica de negócios (Controller) e persistência (DAO).
*/
