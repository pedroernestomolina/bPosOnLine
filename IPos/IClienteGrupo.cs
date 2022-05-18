using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IClienteGrupo
    {

        DtoLib.ResultadoLista<DtoLibPos.ClienteGrupo.Lista.Ficha> ClienteGrupo_GetLista(DtoLibPos.ClienteGrupo.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.ClienteGrupo.Entidad.Ficha> ClienteGrupo_GetFichaById(string id);
        DtoLib.ResultadoAuto ClienteGrupo_Agregar(DtoLibPos.ClienteGrupo.Agregar.Ficha ficha);
        DtoLib.Resultado ClienteGrupo_Editar (DtoLibPos.ClienteGrupo.Editar.Ficha ficha);

    }

}