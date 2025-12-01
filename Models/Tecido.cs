using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Tecido // Classe que representa um tipo de tecido no sistema de estoque
    {
        public int Id { get; set; } // Identificador único do tecido
        public string Cor { get; set; } // Cor do tecido
        public string Tipo { get; set; } // Tipo do tecido (ex: algodão, poliéster)
        public string Textura { get; set; } // Textura do tecido
        public double Qntd_estocada { get; set; } // Quantidade do tecido atualmente estocada
        public double Valor { get; set; } // Valor unitário ou total do tecido
        public int Id_Estoque { get; set; } // ID do estoque ao qual o tecido está vinculado

        // Construtor vazio
        public Tecido() { }

        // Construtor parametrizado para criar um tecido com todos os dados
        public Tecido(int id, string cor, string tipo, string textura, double qntd_estocada, double valor, int id_Estoque)
        {
            Id = id;
            Cor = cor;
            Tipo = tipo;
            Textura = textura;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Id_Estoque = id_Estoque;
        }
    }
}

/*
Resumo técnico:
- Tecido é um Model que representa os tipos de tecidos armazenados no estoque do sistema.
- Contém informações essenciais como ID, cor, tipo, textura, quantidade estocada, valor e estoque associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de tecidos, separando a lógica de negócios (Controller) e persistência (DAO).
*/
