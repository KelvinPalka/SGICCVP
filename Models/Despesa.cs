using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Despesa // Classe que representa uma despesa do sistema
    {
        public string Id { get; set; } // ID da despesa
        public string Descricao { get; set; } // Descrição detalhada da despesa
        public string Data_despesa { get; set; } // Data em que a despesa foi registrada
        public string Data_vencimento { get; set; } // Data de vencimento da despesa
        public string Data_pagamento { get; set; } // Data em que a despesa foi paga
        public string Tipo { get; set; } // Tipo de despesa (ex: fixa, variável)
        public double Valor { get; set; } // Valor financeiro da despesa
        public string Forma_de_pagamento { get; set; } // Forma de pagamento utilizada (ex: cartão, boleto, dinheiro)

        // Construtor vazio
        public Despesa() { }

        // Construtor parametrizado para criar uma despesa com todos os dados
        public Despesa(string id, string descricao, string data_despesa, string data_vencimento, string data_pagamento, string tipo, double valor, string forma_de_pagamento)
        {
            Id = id;
            Descricao = descricao;
            Data_despesa = data_despesa;
            Data_vencimento = data_vencimento;
            Data_pagamento = data_pagamento;
            Tipo = tipo;
            Valor = valor;
            Forma_de_pagamento = forma_de_pagamento;
        }
    }
}

/*
Resumo técnico:
- Despesa é um Model que representa os gastos da empresa no sistema.
- Contém informações essenciais como ID, descrição, datas (registro, vencimento, pagamento), tipo, valor e forma de pagamento.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de despesas, separando a lógica de negócios (Controller) e persistência (DAO).
*/
