using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Permiso.Entidad
{
    
    public class Ficha
    {

        public string estatus { get; set; }
        public string seguridad { get; set; }
        public bool permisoHabilitado { get { return estatus.Trim().ToUpper() == "1"; } }
        public bool requiereClave { get { return seguridad.Trim().ToUpper() != "NINGUNA"; } }


        public Ficha()
        {
            estatus = "";
            seguridad = "";
        }

    }

}