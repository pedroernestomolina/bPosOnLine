using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Resumen
{
    
    public class Ficha
    {

        public int cntMov { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoDivisa { get; set; }
        public int signo { get; set; }
        public string tipoDoc { get; set; }
        public string nombreSuc { get; set; }
        public string codigoSuc { get; set; }


        public Ficha()
        {
            cntMov = 0;
            montoTotal = 0.0m;
            montoDivisa = 0.0m;
            signo = 1;
            tipoDoc = "";
            nombreSuc = "";
            codigoSuc = "";
        }

    }

}