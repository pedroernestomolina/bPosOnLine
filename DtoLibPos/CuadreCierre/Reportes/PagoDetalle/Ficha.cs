using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.PagoDetalle
{
    public class Ficha
    {
        public string estatusCredito { get; set; }
        public string estatusAnulado { get; set; }
        public decimal cambioVueltoMonLocal { get; set; }
        public decimal cambioVueltoMonReferencia{ get; set; }
        public decimal montoRecibidoMonLocal { get; set; }
        public decimal montoRecibidoMonReferencia { get; set; }
        public decimal importeMonLocal { get; set; }
        public decimal importeMonReferencia { get; set; }
        public string hora { get; set; }
        public string nombreRazonSocial { get; set; }
        public string ciRif { get; set; }
        public string dirFiscal { get; set; }
        public string telefonos { get; set; }
        public string nroDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string codigoDoc { get; set; }
        public int signoDoc { get; set; }
        public decimal tasaReferencia { get; set; }
        public string codigoMP { get; set; }
        public string descMP { get; set; }
        public string codigoMoneda { get; set; }
        public string simboloMoneda { get; set; }
        public decimal tasaMoneda { get; set; }
        public decimal montoIngresado { get; set; }
        public string loteNro{ get; set; }
        public string referenciaNro { get; set; }
        public decimal montoIngresadoMonLocal { get; set; }
        public decimal montoIngresadoMonReferencia { get; set; }
    }
}