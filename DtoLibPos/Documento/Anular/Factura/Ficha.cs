using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular.Factura
{
    
    public class Ficha
    {

        public string autoDocumento { get; set; }
        public string autoDocCxC { get; set; }
        public string autoReciboCxC { get; set; }
        public string CodigoDocumento { get; set; }
        public FichaAuditoria auditoria { get; set; }
        public List<FichaDeposito> deposito { get; set; }
        public FichaResumen resumen { get; set; }


        public Ficha()
        {
            autoDocumento = "";
            autoDocCxC = "";
            autoReciboCxC = "";
            CodigoDocumento = "";
            auditoria = new FichaAuditoria();
            deposito = new List<FichaDeposito>();
            resumen = new FichaResumen();
        }

    }

}