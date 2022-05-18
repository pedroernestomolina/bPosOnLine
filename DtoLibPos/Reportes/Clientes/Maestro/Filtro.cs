using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.Clientes.Maestro
{

    public class Filtro
    {

        public string idGrupo { get; set; }
        public string idZona { get; set; }
        public string idEstado { get; set; }
        public string idVendedor { get; set; }
        public string idCobrador { get; set; }
        public string estCategoria { get; set; }
        public string estNivel { get; set; }
        public string estatus { get; set; }
        public string estCredito { get; set; }
        public string estTarifa { get; set; }


        public Filtro()
        {
            idGrupo = "";
            idEstado = "";
            idZona = "";
            idVendedor = "";
            idCobrador = "";
            estatus = "";
            estCategoria = "";
            estCredito = "";
            estNivel = "";
            estTarifa = "";
        }

    }

}