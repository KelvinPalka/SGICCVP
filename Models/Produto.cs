using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Produto // Classe que representa um produto acabado no sistema
    {
        public int Id { get; set; } // Identificador único do produto
        public int Cod_papel { get; set; } // Código do papel utilizado no produto
        public int Cod_tecido { get; set; } // Código do tecido utilizado no produto
        public int Cod_linha { get; set; } // Código da linha utilizada no produto
        public int Cod_fio { get; set; } // Código do fio utilizado no produto
        public int Cod_Tinta { get; set; } // Código da tinta utilizada no produto

        // Construtor vazio
        public Produto() { }

        // Construtor parametrizado para criar um produto com todos os códigos dos materiais
        public Produto(int id, int cod_papel, int cod_tecido, int cod_linha, int cod_fio, int cod_Tinta)
        {
            Id = id;
            Cod_papel = cod_papel;
            Cod_tecido = cod_tecido;
            Cod_linha = cod_linha;
            Cod_fio = cod_fio;
            Cod_Tinta = cod_Tinta;
        }
    }
}

/*
Resumo técnico:
- Produto é um Model que representa os produtos acabados no sistema.
- Contém informações essenciais sobre os códigos dos materiais que compõem o produto: papel, tecido, linha, fio e tinta.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de produtos, separando a lógica de negócios (Controller) e persistência (DAO).
*/
