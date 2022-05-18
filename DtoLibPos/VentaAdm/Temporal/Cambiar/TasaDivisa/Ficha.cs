using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa
{
    
    public class Ficha
    {

        public int id { get; set; }
        public decimal tasaDivisa{ get; set; }
        public decimal montoDivisa { get; set; }
        public List<Item> items { get; set; }


        public Ficha()
        {
            id = -1;
            tasaDivisa =0m;
            montoDivisa = 0m;
            items = new List<Item>();
        }

    }

}
