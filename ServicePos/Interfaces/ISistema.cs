using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface ISistema
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id);
        DtoLib.ResultadoLista<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetLista();

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaById(string id);
        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaByNombre(string nombre);
        DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetLista();

        DtoLib.ResultadoEntidad<string> Sistema_ClaveAcceso_GetByIdNivel(int id);

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Empresa.Ficha> Sistema_Empresa_GetFicha();

        DtoLib.ResultadoLista<DtoLibPos.Sistema.Estado.Entidad.Ficha> Sistema_Estado_GetLista();

    }

}