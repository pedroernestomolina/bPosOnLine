using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPos
{
    
    public interface ICobrador
    {

        DtoLib.ResultadoLista<DtoLibPos.Cobrador.Lista.Ficha> Cobrador_GetLista(DtoLibPos.Cobrador.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Cobrador.Entidad.Ficha> Cobrador_GetFichaById(string id);

    }

}