using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Papel // Classe que representa um tipo de papel no sistema de estoque
    {
        public int Id { get; set; } // Identificador único do papel
        public int Qntd_estocada { get; set; } // Quantidade do papel atualmente estocada
        public double Valor { get; set; } // Valor unitário ou total do papel
        public int Id_estoque { get; set; } // ID do estoque ao qual o papel está vinculado

        // Construtor vazio
        public Papel() { }

        // Construtor parametrizado para criar um papel com todos os dados
        public Papel(int id, int qntd_estocada, double valor, int id_estoque)
        {
            Id = id;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Id_estoque = id_estoque;
        }
    }
}

/*
Resumo técnico:
- Papel é um Model que representa os tipos de papéis armazenados no estoque do sistema.
- Contém informações essenciais como ID, quantidade estocada, valor e estoque associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de papéis, separando a lógica de negócios (Controller) e persistência (DAO).
*/
