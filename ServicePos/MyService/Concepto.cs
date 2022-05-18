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

        public DtoLib.ResultadoLista<DtoLibPos.Concepto.Lista.Ficha> Concepto_GetLista(DtoLibPos.Concepto.Lista.Filtro filtro)
        {
            return ServiceProv.Concepto_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Concepto.Entidad.Ficha> Concepto_GetFichaById(string id)
        {
            return ServiceProv.Concepto_GetFichaById(id);
        }

    }

}