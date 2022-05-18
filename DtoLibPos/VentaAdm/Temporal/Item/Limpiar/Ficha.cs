using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Item.Limpiar
{
    
    public class Ficha 
    {

        public ItemEncabezado itemEncabezado { get; set; }
        public List<ItemDetalle> itemDetalle { get; set; }
        public List<ItemActDeposito> itemActDeposito { get; set; }


        public Ficha() 
        {
            itemEncabezado = new ItemEncabezado();
            itemDetalle = new List<ItemDetalle>();
            itemActDeposito = null;
        }

    }

}