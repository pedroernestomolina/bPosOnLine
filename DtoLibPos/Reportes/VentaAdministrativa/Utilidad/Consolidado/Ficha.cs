using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado
{
    
    public class Ficha
    {

        public int signoDoc { get; set; }
        public string tipoDoc { get; set; }
        public string nombreDoc { get; set; }
        public string nombreSuc { get; set; }
        public string codigoSuc { get; set; }
        public decimal vCosto { get; set; }
        public decimal vVenta { get; set; }
        public decimal vUtilidad { get; set; }


        public Ficha()
        {
            tipoDoc = "";
            signoDoc = 1;
            nombreDoc = "";
            codigoSuc = "";
            nombreSuc = "";
            vCosto = 0m;
            vVenta = 0m;
            vUtilidad = 0m;
        }

    }

}