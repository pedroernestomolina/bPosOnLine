using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.Eliminar
{
    
    public class Ficha
    {
        
        public int idOperador { get; set; }
        public int idItem { get; set; }
        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }
        public decimal cantUndBloq { get; set; }


        public Ficha()
        {
            idOperador = -1;
            idItem = -1;
            autoProducto = "";
            autoDeposito = "";
            cantUndBloq = 0.0m;
        }

    }

}
