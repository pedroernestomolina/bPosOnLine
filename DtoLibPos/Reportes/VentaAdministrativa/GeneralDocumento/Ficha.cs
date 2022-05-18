using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public string control { get; set; }
        public string serie { get; set; }
        public string estatusDoc { get; set; }
        public string clienteNombre { get; set; }
        public string clienteCiRif { get; set; }
        public decimal total { get; set; }
        public string tipoDoc { get; set; }
        public decimal totalDivisa { get; set; }
        public int renglones { get; set; }
        public decimal factorDoc { get; set; }
        public int signoDoc { get; set; }
        public string nombreDoc { get; set; }
        public decimal montoDscto { get; set; }
        public decimal montoCargo { get; set; }
        public string sucNombre { get; set; }
        public string sucCodigo { get; set; }
        public string estacion { get { return auto.Substring(2, 2); } }


        public Ficha()
        {
            auto = "";
            fecha = DateTime.Now.Date;
            documento = "";
            control = "";
            serie = "";
            estatusDoc = "";
            clienteNombre = "";
            clienteCiRif = "";
            total = 0.0m;
            tipoDoc = "";
            totalDivisa = 0.0m;
            factorDoc = 0.0m;
            signoDoc = 1;
            nombreDoc = "";
            montoDscto = 0.0m;
            montoCargo = 0.0m;
            sucCodigo = "";
            sucNombre = "";
        }

    }

}