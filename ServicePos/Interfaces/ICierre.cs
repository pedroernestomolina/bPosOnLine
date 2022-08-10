using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface ICierre
    {

        DtoLib.ResultadoLista<DtoLibPos.Pos.Cierre.Lista.Ficha>
          Cierre_Lista_GetByFiltro(DtoLibPos.Pos.Cierre.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Pos.Cierre.Entidad.Ficha>
            Cierre_GetById(int idCierre);

    }

}