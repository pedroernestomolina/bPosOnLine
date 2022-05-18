using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IConcepto
    {

        DtoLib.ResultadoLista<DtoLibPos.Concepto.Lista.Ficha> Concepto_GetLista(DtoLibPos.Concepto.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Concepto.Entidad.Ficha> Concepto_GetFichaById(string id);

    }

}