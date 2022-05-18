using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaCredito
{
    
    public class FichaKardex
    {

        public string AutoProducto { get; set; }
        public decimal Total { get; set; }
        public string AutoDeposito { get; set; }
        public string AutoConcepto { get; set; }
        public string Modulo { get; set; }
        public string Entidad { get; set; }
        public int Signo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadBono { get; set; }
        public decimal CantidadUnd { get; set; }
        public decimal CostoUnd { get; set; }
        public string EstatusAnulado { get; set; }
        public string Nota { get; set; }
        public decimal PrecioUnd { get; set; }
        public string Codigo { get; set; }
        public string Siglas { get; set; }
        public string CodigoSucursal { get; set; }
        public string CierreFtp { get; set; }
        public string CodigoDeposito { get; set; }
        public string NombreDeposito { get; set; }
        public string CodigoConcepto { get; set; }
        public string NombreConcepto { get; set; }


        public FichaKardex()
        {
            AutoProducto = "";
            Total = 0.0m;
            AutoDeposito = "";
            AutoConcepto = "";
            Modulo = "";
            Entidad = "";
            Signo = 1;
            Cantidad = 0.0m;
            CantidadBono = 0.0m;
            CantidadUnd = 0.0m;
            CostoUnd = 0.0m;
            EstatusAnulado = "";
            Nota = "";
            PrecioUnd = 0.0m;
            Codigo = "";
            Siglas = "";
            CodigoSucursal = "";
            CierreFtp = "";
            CodigoDeposito = "";
            NombreDeposito = "";
            CodigoConcepto = "";
            NombreConcepto = "";
        }

    }

}