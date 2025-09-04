using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IMoneda
    {
        DtoLib.ResultadoLista<DtoLibPos.Moneda.Entidad.Ficha>
            Moneda_GetLista(DtoLibPos.Moneda.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Moneda.Entidad.Ficha>
            Moneda_GetFichaById(int id);
    }
}