using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Tinta // Classe que representa um tipo de tinta no sistema de estoque
    {
        public int Id { get; set; } // Identificador único da tinta
        public string Cor { get; set; } // Cor da tinta
        public double Qntd_estocada { get; set; } // Quantidade da tinta atualmente estocada
        public double Valor { get; set; } // Valor unitário ou total da tinta
        public string Tipo { get; set; } // Tipo da tinta (ex: aquarela, acrílica)
        public int Id_estoque { get; private set; } // ID do estoque ao qual a tinta está vinculada (somente leitura)

        // Construtor vazio
        public Tinta() { }

        // Construtor parametrizado para criar uma tinta com todos os dados
        public Tinta(int id, string cor, double qntd_estocada, double valor, string tipo, int id_estoque)
        {
            Id = id;
            Cor = cor;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Tipo = tipo;
            Id_estoque = id_estoque;
        }
    }
}

/*
Resumo técnico:
- Tinta é um Model que representa os tipos de tintas armazenadas no estoque do sistema.
- Contém informações essenciais como ID, cor, quantidade estocada, valor, tipo e estoque associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de tintas, separando a lógica de negócios (Controller) e persistência (DAO).
*/
