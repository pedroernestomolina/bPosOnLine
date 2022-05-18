using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Abrir
{
    
    public class Operador
    {

        public string idUsuario { get; set; }
        public string idEquipo { get; set; }
        public string estatus { get; set; }
        public string codSucursal { get; set; }


        public Operador()
        {
            idUsuario = "";
            idEquipo = "";
            estatus = "";
            codSucursal = "";
        }

    }

}