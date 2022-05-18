using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IMedioPago
    {

        DtoLib.ResultadoLista<DtoLibPos.MedioPago.Lista.Ficha> MedioPago_GetLista(DtoLibPos.MedioPago.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.MedioPago.Entidad.Ficha> MedioPago_GetFichaById(string id);

    }

}