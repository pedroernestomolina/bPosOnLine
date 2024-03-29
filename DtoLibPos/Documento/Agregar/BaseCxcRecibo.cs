﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar
{
    
    public class BaseCxCRecibo
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
        // CAMPOS NUEVOS
        public decimal ImporteDivisa { get; set; }
        public decimal MontoRecibidoDivisa { get; set; }
        public decimal CambioDivisa { get; set; }
        public string CodigoSucursal { get; set; }

    }

}