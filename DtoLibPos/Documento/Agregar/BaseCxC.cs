using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar
{
    
    public class BaseCxC
    {

        public decimal CCobranza { get; set; }
        public decimal CCobranzap { get; set; }
        public string TipoDocumento { get; set; }
        public string Nota { get; set; }
        public decimal Importe { get; set; }
        public decimal Acumulado { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string CodigoCliente { get; set; }
        public string EstatusCancelado { get; set; }
        public decimal Resta { get; set; }
        public string EstatusAnulado { get; set; }
        public string Numero { get; set; }
        public string AutoAgencia { get; set; }
        public string Agencia { get; set; }
        public int Signo { get; set; }
        public string AutoVendedor { get; set; }
        public decimal CDepartamento { get; set; }
        public decimal CVentas { get; set; }
        public decimal CVentasp { get; set; }
        public string Serie { get; set; }
        public decimal ImporteNeto { get; set; }
        public int Dias { get; set; }
        public decimal CastigoP { get; set; }
        public string CierreFtp { get; set; }
        public decimal MontoDivisa { get; set; }
        public decimal TasaDivisa { get; set; }
        //CAMPOS NUEVOS 
        public decimal AcumuladoDivisa { get; set; }
        public string CodigoSucursal { get; set; }
        public decimal RestaDivisa { get; set; }
        public decimal ImporteNetoDivisa { get; set; }

    }

}