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
        public DtoLib.ResultadoAuto 
            Agencia_Agregar(DtoLibPos.Agencia.Agregar.Ficha ficha)
        {
            return ServiceProv.Agencia_Agregar(ficha);
        }
        public DtoLib.Resultado 
            Agencia_Editar(DtoLibPos.Agencia.Editar.Ficha ficha)
        {
            return ServiceProv.Agencia_Editar(ficha);
        }


        public DtoLib.ResultadoLista<DtoLibPos.Agencia.Entidad.Ficha> 
            Agencia_GetLista(DtoLibPos.Agencia.Lista.Filtro filtro)
        {
            return ServiceProv.Agencia_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Agencia.Entidad.Ficha>
            Agencia_GetFichaById(string id)
        {
            return ServiceProv.Agencia_GetFichaById(id);
        }

    }

}
