using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoLista<DtoLibPos.ClienteZona.Lista.Ficha> ClienteZona_GetLista(DtoLibPos.ClienteZona.Lista.Filtro filtro)
        {
            return ServiceProv.ClienteZona_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ClienteZona.Entidad.Ficha> ClienteZona_GetFichaById(string id)
        {
            return ServiceProv.ClienteZona_GetFichaById(id);
        }

        public DtoLib.ResultadoAuto ClienteZona_Agregar(DtoLibPos.ClienteZona.Agregar.Ficha ficha)
        {
            return ServiceProv.ClienteZona_Agregar(ficha);
        }

        public DtoLib.Resultado ClienteZona_Editar(DtoLibPos.ClienteZona.Editar.Ficha ficha)
        {
            return ServiceProv.ClienteZona_Editar(ficha);
        }

    }

}