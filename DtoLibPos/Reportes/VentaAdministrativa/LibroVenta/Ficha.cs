using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.LibroVenta
{
    
    public class Ficha
    {

        public string codigoSucursalDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public string ciRifDoc { get; set; }
        public string nombreRazonSocialDoc { get; set; }
        public string numDoc { get; set; }
        public string numControlDoc { get; set; }
        public string codigoDoc { get; set; }
        public string numAplicaDoc { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoExento { get; set; }
        public decimal montoBase1 { get; set; }
        public decimal montoBase2 { get; set; }
        public decimal montoImpuesto1 { get; set; }
        public decimal montoImpuesto2 { get; set; }
        public decimal tasaIva1 { get; set; }
        public decimal tasaIva2 { get; set; }
        public int signoDoc { get; set; }
        public decimal montoRetencionIva { get; set; }
        public decimal tasaRetencionIva { get; set; }
        public DateTime fechaRetencionIva { get; set; }
        public string comprobanteRetencionIva {get;set;}
        public string auto { get; set; }


        public Ficha() 
        {
            codigoSucursalDoc = "";
            fechaDoc = DateTime.Now.Date;
            ciRifDoc = "";
            nombreRazonSocialDoc = "";
            numDoc = "";
            numControlDoc = "";
            codigoDoc = "";
            numAplicaDoc = "";
            montoTotal = 0m;
            montoExento = 0m;
            montoBase1 = 0m;
            montoBase2 = 0m;
            montoImpuesto1 = 0m;
            montoImpuesto2 = 0m;
            tasaIva1 = 0m;
            tasaIva2 = 0m;
            signoDoc = 1;
            montoRetencionIva = 0m;
            tasaRetencionIva = 0m;
            fechaRetencionIva = DateTime.Now.Date;
            comprobanteRetencionIva = "";
            auto = "";
        }

    }

}