
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.RecopilarData.Anular
{
    public class Documento
    {
        public string idDoc { get; set; }
        public string codigoDoc { get; set; }
        public string idDocCxc { get; set; }
        public string idReciboCxc { get; set; }
        public string idCliente { get; set; }
        public decimal montoPendCxc { get; set; }
        public string estatusAnulado { get; set; }
        public string estatusCredito { get; set; }
        public string estatusDocFiscal { get; set; }
    }
}