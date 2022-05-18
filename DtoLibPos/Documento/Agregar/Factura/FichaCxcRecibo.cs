using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class FichaCxCRecibo
    {

        public string AutoUsuario { get; set; }
        public decimal Importe { get; set; }
        public string Usuario { get; set; }
        public decimal MontoRecibido { get; set; }
        public string Cobrador { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string Codigo { get; set; }
        public string EstatusAnulado { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string AutoCobrador { get; set; }
        public decimal Anticipos { get; set; }
        public decimal Cambio { get; set; }
        public string Nota { get; set; }
        public string CodigoCobrador { get; set; }
        public decimal Retenciones { get; set; }
        public decimal Descuentos { get; set; }
        public string Cierre { get; set; }
        public string CierreFtp { get; set; }


        public FichaCxCRecibo()
        {
            AutoUsuario = "";
            Importe = 0.0m;
            Usuario = "";
            MontoRecibido = 0.0m;
            Cobrador = "";
            AutoCliente = "";
            Cliente = "";
            CiRif = "";
            Codigo = "";
            EstatusAnulado = "";
            Direccion = "";
            Telefono = "";
            AutoCobrador = "";
            Anticipos = 0.0m;
            Cambio = 0.0m;
            Nota = "";
            CodigoCobrador = "";
            Retenciones = 0.0m;
            Descuentos = 0.0m;
            Cierre = "";
            CierreFtp = "";
        }

    }

}