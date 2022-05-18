using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar
{
    
    public class Ficha
    {

        public string idEquipo { get; set; }
        public string autoUsuario { get; set; }
        public string autoCliente { get; set; }
        public string autoSucursal { get; set; }
        public string autoDeposito { get; set; }
        public string autoSistDocumento { get; set; }
        public string ciRifCliente { get; set; }
        public string razonSocialCliente { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal factorDivisa {get;set;}
        public int renglones { get; set; }
        public string estatusPendiente { get; set; }
        public string nombreSistDocumento { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreDeposito { get; set; }
        public string nombreSucursal { get; set; }
        //
        public string codigoCliente { get; set; }
        public string dirFiscalCliente { get; set; }
        public string tarifaPrecioCliente { get; set; }
        public string estatusCredito { get; set; }
        public int diasCredito { get; set; }
        public string autoVendedor { get; set; }
        public string autoCobrador { get; set; }
        public string autoTransporte { get; set; }
        public int diasValidez { get; set; }
        public string dirDespacho { get; set; }
        public string notasDoc { get; set; }
        public string tipoRemision { get; set; }
        public string documentoRemision { get; set; }
        public string autoRemision { get; set; }
        public string nombreTipoDocRemision { get; set; }


        public Ficha() 
        {
            idEquipo = "";
            autoCliente = "";
            autoDeposito = "";
            autoSucursal = "";
            autoSistDocumento = "";
            ciRifCliente = "";
            razonSocialCliente = "";
            monto = 0m;
            montoDivisa = 0m;
            factorDivisa = 0m;
            renglones = 0;
            estatusPendiente = "";
            nombreSistDocumento = "";
            nombreUsuario = "";
            nombreDeposito = "";
            nombreSucursal = "";
            //
            codigoCliente = "";
            dirFiscalCliente = "";
            tarifaPrecioCliente = "";
            estatusCredito = "";
            diasCredito = 0;
            autoVendedor = "";
            autoCobrador = "";
            autoTransporte = "";
            diasValidez = 0;
            dirDespacho = "";
            notasDoc = "";
            tipoRemision = "";
            documentoRemision = "";
            autoRemision = "";
            nombreTipoDocRemision = "";
        }

    }

}