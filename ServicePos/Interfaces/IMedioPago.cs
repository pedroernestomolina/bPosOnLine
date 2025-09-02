using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IMedioPago
    {
        DtoLib.ResultadoLista<DtoLibPos.MedioPago.Entidad.Ficha> 
            MedioPago_GetLista(DtoLibPos.MedioPago.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.MedioPago.Entidad.Ficha> 
            MedioPago_GetFichaById(string id);
    }
}