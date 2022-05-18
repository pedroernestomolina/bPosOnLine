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

        public DtoLib.ResultadoLista<DtoLibPos.Transporte.Lista.Ficha> Transporte_GetLista(DtoLibPos.Transporte.Lista.Filtro filtro)
        {
            return ServiceProv.Transporte_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Transporte.Entidad.Ficha> Transporte_GetFichaById(string id)
        {
            return ServiceProv.Transporte_GetFichaById(id);
        }

    }

}