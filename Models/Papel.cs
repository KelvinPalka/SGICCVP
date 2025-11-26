using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Papel
    {
        public int Id {  get; set; }
        public int Qntd_estocada { get; set; }
        public double Valor { get; set; }
        public int Id_estoque {  get; set; }

        public Papel() { }

        public Papel(int id, int qntd_estocada, double valor, int id_estoque)
        {
            Id = id;
            Qntd_estocada = qntd_estocada;
            Valor = valor;
            Id_estoque = id_estoque;
        }
    }
}
