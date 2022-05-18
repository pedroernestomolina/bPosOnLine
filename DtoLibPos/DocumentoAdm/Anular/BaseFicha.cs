using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Anular
{
    
    public abstract class BaseFicha
    {

        public string autoDocumento { get; set; }
        public BaseAuditoria auditoria { get; set; }

    }

}