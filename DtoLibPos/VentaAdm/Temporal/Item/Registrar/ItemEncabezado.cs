using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Item.Registrar
{
    
    public class ItemEncabezado
    {

        public int id { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int renglones { get; set; }


        public ItemEncabezado()
        {
            id = -1;
            monto = 0m;
            montoDivisa = 0m;
            renglones = 0;
        }

    }

}