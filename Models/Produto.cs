using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Wpf_Projeto_BD.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int Cod_papel { get; set; }
        public int Cod_tecido { get; set; }
        public int Cod_linha { get; set; }
        public int Cod_fio { get; set; }
        public int Cod_Tinta { get; set; }

        public Produto() { }    

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
