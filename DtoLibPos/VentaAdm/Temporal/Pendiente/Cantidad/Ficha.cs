using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Pendiente.Cantidad
{
    
    public class Ficha
    {

        public string autoUsuario { get; set; }
        public string autoSistDocumento { get; set; }
        public string idEquipo { get; set; }


        public Ficha() 
        {
            autoSistDocumento = "";
            autoUsuario = "";
            idEquipo = "";
        }
            
    }

}