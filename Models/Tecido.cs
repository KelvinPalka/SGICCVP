using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Tecido
    {
        public int Id { get; set; }
        public string Cor {  get; set; }
        public string Tipo { get; set; }
        public string Textura { get; set; }
        public double Qntd_estocada { get; set; }
        public double Valor { get; set; }
        public int Id_Estoque { get; set; }

        public Tecido() { }

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
