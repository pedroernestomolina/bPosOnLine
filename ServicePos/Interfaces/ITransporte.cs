using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface ITransporte
    {

        DtoLib.ResultadoLista<DtoLibPos.Transporte.Lista.Ficha> Transporte_GetLista(DtoLibPos.Transporte.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Transporte.Entidad.Ficha> Transporte_GetFichaById(string id);

    }

}