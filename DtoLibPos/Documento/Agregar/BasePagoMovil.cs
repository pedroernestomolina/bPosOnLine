using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar
{
    
    public abstract class BasePagoMovil
    {

        public string autoAgencia { get; set; }
        public string nombre { get; set; }
        public string ciRif { get; set; }
        public string telefono { get; set; }
        public decimal monto { get; set; }
        //
        public string codigoSucursal { get; set; }
        public string nombreAgencia { get; set; }
        public string clienteNombre { get; set; }
        public string clienteRif { get; set; }
        public string clienteDirFiscal { get; set; }
        public decimal montoDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string codigoDocumento { get; set; }

    }

}