using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular 
{
    
    public class Auditoria
    {

        public string autoSistemaDocumento { get; set; }
        public string autoUsuario { get; set; }
        public string usuario { get; set; }
        public string codigo { get; set; }
        public string estacion { get; set; }
        public string motivo { get; set; }


        public Auditoria()
        {
            autoSistemaDocumento = "";
            autoUsuario = "";
            usuario = "";
            codigo = "";
            estacion = "";
            motivo = "";
        }

    }

}