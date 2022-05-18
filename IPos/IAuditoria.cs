using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IAuditoria
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Auditoria.Entidad.Ficha> Auditoria_Documento_GetFichaBy(DtoLibPos.Auditoria.Buscar.Ficha ficha);

    }

}