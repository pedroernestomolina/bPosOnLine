using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IVendedor
    {

        DtoLib.ResultadoLista<DtoLibPos.Vendedor.Lista.Ficha> Vendedor_GetLista(DtoLibPos.Vendedor.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Vendedor.Entidad.Ficha> Vendedor_GetFichaById(string id);

    }

}