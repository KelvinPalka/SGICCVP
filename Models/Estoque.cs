using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Projeto_BD.Models
{
    public class Estoque
    {
        public int Id {  get; set; }
        public string Data {  get; set; }
        public string Local_armazenamento { get; set; }

        public Estoque() { }

        public Estoque(int id, string data, string local_armazenamento)
        {
            Id = id;
            Data = data;
            Local_armazenamento = local_armazenamento;
        }
    }
}
