using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular.NotaEntrega
{
    
    public class Ficha
    {

        public string autoDocumento { get; set; }
        public string CodigoDocumento { get; set; }
        public FichaAuditoria auditoria { get; set; }
        public List<FichaDeposito> deposito { get; set; }
        public FichaResumen resumen { get; set; }


        public Ficha()
        {
            autoDocumento = "";
            CodigoDocumento = "";
            auditoria = new FichaAuditoria();
            deposito = new List<FichaDeposito>();
            resumen = new FichaResumen();
        }

    }

}