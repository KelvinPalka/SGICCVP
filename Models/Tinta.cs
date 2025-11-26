using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Tinta
    {
        public int Id {  get; set; }  
        public string Cor {  get; set; }
        public double Qntd_estocada { get; set; }
        public double Valor {  get; set; }
        public string Tipo { get; set; }
        public int Id_estoque { get; private set; }

        public Tinta() { }

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
