using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface ISistema
    {
        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id);
        DtoLib.ResultadoLista<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetLista();

        DtoLib.ResultadoEntidad<string> Sistema_ClaveAcceso_GetByIdNivel(int id);

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Empresa.Ficha> Sistema_Empresa_GetFicha();

        DtoLib.ResultadoLista<DtoLibPos.Sistema.Estado.Entidad.Ficha> Sistema_Estado_GetLista();
    }
}