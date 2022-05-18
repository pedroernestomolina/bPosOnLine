using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.PagoDetalle
{
    
    public class Ficha
    {

        public string autoRecibo { get; set; }
        public string medioPagoCodigo { get; set; }
        public string medioPagoDesc { get; set; }
        public string loteCntDivisa { get; set; }
        public string referenciaTasa { get; set; }
        public decimal montoRecibido { get; set; }
        public DateTime documentoFecha { get; set; }
        public string documentoTipo { get; set; }
        public string documentoNro { get; set; }
        public string clienteNombre { get; set; }
        public string clienteCiRif { get; set; }
        public string clienteDir { get; set; }
        public string clienteTelf { get; set; }
        public string hora { get; set; }
        public decimal importe { get; set; }
        public decimal cambioDar { get; set; }
        public string estatus { get; set; }


        public Ficha() 
        {
            autoRecibo = "";
            medioPagoCodigo = "";
            medioPagoDesc = "";
            loteCntDivisa = "";
            referenciaTasa = "";
            montoRecibido = 0.0m;
            documentoFecha = DateTime.Now.Date;
            documentoTipo = "";
            documentoNro = "";
            clienteNombre = "";
            clienteCiRif = "";
            clienteDir = "";
            clienteTelf = "";
            hora = "";
            importe = 0.0m;
            cambioDar = 0.0m;
            estatus = "";
        }

    }

}