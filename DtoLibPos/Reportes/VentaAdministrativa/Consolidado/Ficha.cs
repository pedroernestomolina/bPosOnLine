using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Consolidado
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public DateTime fecha { get; set; }
        public string codigoSuc { get; set; }
        public string nombreSuc { get; set; }
        public string documento { get; set; }
        public string tipo { get; set; }
        public string aplica { get; set; }
        public decimal total { get; set; }
        public decimal totalDivisa { get; set; }
        public decimal factor { get; set; }
        public string docNombre { get; set; }
        public int signo { get; set; }
        public int caja { get { return int.Parse(auto.Substring(2, 2)); } }


        public Ficha()
        {
            auto = "";
            fecha = DateTime.Now.Date;
            codigoSuc = "";
            nombreSuc = "";
            documento = "";
            tipo = "";
            aplica = "";
            total = 0.0m;
            totalDivisa = 0.0m;
            factor = 0.0m;
            docNombre = "";
            signo = 1;
        }

    }

}