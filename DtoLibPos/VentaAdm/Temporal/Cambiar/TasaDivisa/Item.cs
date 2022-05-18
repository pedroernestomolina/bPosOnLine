using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa
{
    
    public class Item
    {

        public int id { get; set; }
        public string descProducto { get; set; }
        public decimal totalDivisa { get; set; }


        public Item()
        {
            id = -1;
            descProducto = "";
            totalDivisa = 0m;
        }

    }

}