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
        public DtoLib.ResultadoLista<DtoLibPos.ClienteGrupo.Lista.Ficha> ClienteGrupo_GetLista(DtoLibPos.ClienteGrupo.Lista.Filtro filtro)
        {
            return ServiceProv.ClienteGrupo_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ClienteGrupo.Entidad.Ficha> ClienteGrupo_GetFichaById(string id)
        {
            return ServiceProv.ClienteGrupo_GetFichaById(id);
        }

        public DtoLib.ResultadoAuto ClienteGrupo_Agregar(DtoLibPos.ClienteGrupo.Agregar.Ficha ficha)
        {
            return ServiceProv.ClienteGrupo_Agregar(ficha);
        }

        public DtoLib.Resultado ClienteGrupo_Editar(DtoLibPos.ClienteGrupo.Editar.Ficha ficha)
        {
            return ServiceProv.ClienteGrupo_Editar(ficha);
        }

    }

}