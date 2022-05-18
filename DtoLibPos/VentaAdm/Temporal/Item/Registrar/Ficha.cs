using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Item.Registrar
{
    
    public class Ficha 
    {

        public bool validarExistencia { get; set; }
        public ItemEncabezado itemEncabezado { get; set; }
        public ItemDetalle itemDetalle { get; set; }
        public ItemActDeposito itemActDeposito { get; set; }


        public Ficha() 
        {
            itemEncabezado = new ItemEncabezado();
            itemDetalle = new ItemDetalle();
            itemActDeposito = null;
            validarExistencia = false;
        }

    }

}