using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IClienteZona
    {

        DtoLib.ResultadoLista<DtoLibPos.ClienteZona.Lista.Ficha> ClienteZona_GetLista(DtoLibPos.ClienteZona.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.ClienteZona.Entidad.Ficha> ClienteZona_GetFichaById(string id);
        DtoLib.ResultadoAuto ClienteZona_Agregar(DtoLibPos.ClienteZona.Agregar.Ficha ficha);
        DtoLib.Resultado ClienteZona_Editar(DtoLibPos.ClienteZona.Editar.Ficha ficha);

    }

}