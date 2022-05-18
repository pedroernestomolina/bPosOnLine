using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Anular
{
    
    public abstract class BaseAuditoria
    {

        public string autoSistemaDocumento { get; set; }
        public string autoUsuario { get; set; }
        public string usuario { get; set; }
        public string codigo { get; set; }
        public string estacion { get; set; }
        public string motivo { get; set; }

    }

}