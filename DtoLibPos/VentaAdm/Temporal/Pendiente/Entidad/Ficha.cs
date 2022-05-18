using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad
{
    
    public class Ficha
    {

        public Encabezado.Entidad.Ficha encabezado { get; set; }
        public List<Item.Entidad.Ficha> items { get; set; }


        public Ficha() 
        {
            encabezado = new Encabezado.Entidad.Ficha();
            items = new List<Item.Entidad.Ficha>();
        }

    }

}