using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Item.Registrar
{
    
    public class ItemDetalle
    {

        public int idVenta { get; set; }
        public string autoProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoSubGrupo { get; set; }
        public string autoTasaIva { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal cantidad { get; set; }
        public decimal precioNeto { get; set; }
        public decimal precioNetoDivisa { get; set; }
        public string tarifaPrecio { get; set; }
        public decimal tasaIva { get; set; }
        public string tipoIva { get; set; }
        public string categroiaProducto { get; set; }
        public string decimalesProducto { get; set; }
        public string empaqueDesc { get; set; }
        public int empaqueCont { get; set; }
        public string estatusPesadoProducto { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromd { get; set; }
        public decimal costoPromdUnd { get; set; }
        public decimal dsctoPorct { get; set; }
        public string notas { get; set; }
        public string estatusReservaMerc { get; set; }
        public decimal total { get; set; }
        public decimal totalDivisa { get; set; }
        public decimal cantidadUnd { get; set; }
        public string autoDeposito { get; set; }
        public string estatusRemision { get; set; }
        public string nombreDeposito { get; set; }


        public ItemDetalle() 
        {
            idVenta = -1;
            autoDepartamento = "";
            autoGrupo = "";
            autoProducto = "";
            autoSubGrupo = "";
            autoTasaIva = "";
            codigoProducto = "";
            nombreProducto = "";
            cantidad = 0m;
            precioNeto = 0m;
            precioNetoDivisa = 0m;
            tarifaPrecio = "";
            tasaIva = 0m;
            tipoIva = "";
            categroiaProducto = "";
            decimalesProducto = "";
            empaqueCont = 0;
            empaqueDesc = "";
            estatusPesadoProducto = "";
            estatusReservaMerc = "";
            costo = 0m;
            costoPromd = 0m;
            costoPromdUnd = 0m;
            costoUnd = 0m;
            dsctoPorct = 0m;
            notas = "";
            total = 0m;
            totalDivisa = 0m;
            cantidadUnd = 0m;
            autoDeposito = "";
            estatusRemision = "";
            nombreDeposito = "";
        }

    }

}