using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IAgencia
    {

        DtoLib.ResultadoAuto
            Agencia_Agregar(DtoLibPos.Agencia.Agregar.Ficha ficha);
        DtoLib.Resultado
            Agencia_Editar(DtoLibPos.Agencia.Editar.Ficha ficha);


        DtoLib.ResultadoLista<DtoLibPos.Agencia.Entidad.Ficha>
            Agencia_GetLista(DtoLibPos.Agencia.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Agencia.Entidad.Ficha>
            Agencia_GetFichaById(string id);

    }

}