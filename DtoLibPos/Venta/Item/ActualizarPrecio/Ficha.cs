using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.ActualizarPrecio
{
    
    public class Ficha
    {

        public int idOperador { get; set; }
        public int idItem { get; set; }
        public decimal pNeto { get; set; }
        public decimal pDivisa { get; set; }
        public string tarifaVenta { get; set; }


        public Ficha()
        {
            idOperador = -1;
            idItem = -1;
            pNeto = 0m;
            pDivisa = 0m;
            tarifaVenta = "";
        }

    }

}