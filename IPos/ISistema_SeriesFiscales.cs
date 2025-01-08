using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface ISistema_SeriesFiscales
    {
        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetFichaById(string id);
        DtoLib.ResultadoEntidad<string> 
            Sistema_Serie_GetFichaByNombre(string nombre);
        DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetLista();
    }
}