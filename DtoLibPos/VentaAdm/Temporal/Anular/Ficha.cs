using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Anular
{
    
    public class Ficha
    {

        public int IdEncabezado { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemActDeposito> ItemsActDeposito { get; set; }


        public Ficha() 
        {
            IdEncabezado = -1;
            Items = null;
            ItemsActDeposito = null;
        }

    }

}