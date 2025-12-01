using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    internal class Fio // Classe que representa um tipo de fio no sistema de estoque
    {
        public int Id { get; set; } // Identificador único do fio
        public double Qntd_estocada { get; set; } // Quantidade do fio atualmente estocada
        public double Valor { get; set; } // Valor unitário ou total do fio
        public int Id_estoque { get; } // ID do estoque ao qual o fio está vinculado (somente leitura)

        // Construtor vazio
        public Fio() { }

        // Construtor parametrizado para criar um fio com todos os dados
        public Fio(int id, double qntd_estocada, double valor, int id_estoque)
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
- Fio é um Model que representa os tipos de fios armazenados no estoque do sistema.
- Contém informações essenciais como ID, quantidade estocada, valor e estoque associado.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de fios, separando a lógica de negócios (Controller) e persistência (DAO).
*/
