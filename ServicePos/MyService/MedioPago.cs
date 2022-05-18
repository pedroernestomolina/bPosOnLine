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

        public DtoLib.ResultadoLista<DtoLibPos.MedioPago.Lista.Ficha> MedioPago_GetLista(DtoLibPos.MedioPago.Lista.Filtro filtro)
        {
            return ServiceProv.MedioPago_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.MedioPago.Entidad.Ficha> MedioPago_GetFichaById(string id)
        {
            return ServiceProv.MedioPago_GetFichaById(id);
        }

    }

}