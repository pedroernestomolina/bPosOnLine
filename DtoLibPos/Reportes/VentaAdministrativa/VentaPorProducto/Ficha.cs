using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto
{

    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal cantidad { get; set; }
        public decimal totalMonto { get; set; }
        public decimal totalMontoDivisa { get; set; }
        public string nombreDocumento { get; set; }
        public int signo { get; set; }


        public Ficha()
        {
            codigoPrd = "";
            nombrePrd = "";
            cantidad = 0;
            totalMonto = 0.0m;
            totalMontoDivisa = 0.0m;
            nombreDocumento = "";
            signo = 1;
        }

    }

}