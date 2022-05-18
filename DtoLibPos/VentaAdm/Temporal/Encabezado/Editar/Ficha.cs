using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Encabezado.Editar
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string autoCliente { get; set; }
        public string autoSucursal { get; set; }
        public string autoDeposito { get; set; }
        public string ciRifCliente { get; set; }
        public string razonSocialCliente { get; set; }
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


        public Ficha() 
        {
            id = -1;
            autoCliente = "";
            autoDeposito = "";
            autoSucursal = "";
            ciRifCliente = "";
            razonSocialCliente = "";
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
        }

    }

}