using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    internal class Fio
    {
        public int Id {  get; set; }
        public double Qntd_estocada { get; set; }
        public double Valor {  get; set; }
        public int Id_estoque { get; }

        public Fio() { }

        public Fio(int id, double qntd_estocada, double valor, int id_estoque)
        {
            Id = id;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Id_estoque = id_estoque;
        }
    }
}
