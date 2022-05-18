using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Item.Actualizar
{
    
    public class Ficha
    {

        public Eliminar.Ficha itemEliminar { get; set; }
        public Registrar.Ficha itemRegistrar { get; set; }
        

        public Ficha() 
        {
            itemEliminar = new Eliminar.Ficha();
            itemRegistrar = new Registrar.Ficha();
        }

    }

}