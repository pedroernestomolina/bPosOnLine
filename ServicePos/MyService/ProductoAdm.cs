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

        public DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.Lista.Ficha> ProductoAdm_GetLista(DtoLibPos.ProductoAdm.Lista.Filtro filtro)
        {
            return ServiceProv.ProductoAdm_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Entidad.Ficha> ProductoAdm_GetFichaById(string id)
        {
            return ServiceProv.ProductoAdm_GetFichaById(id);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Existencia.Ficha> ProductoAdm_Existencia_GetFichaByDeposito(string idPrd, string idDeposito)
        {
            return ServiceProv.ProductoAdm_Existencia_GetFichaByDeposito(idPrd, idDeposito);
        }

        public DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.ListaResumen.Ficha> ProductoAdm_GetListaResumen(DtoLibPos.ProductoAdm.ListaResumen.Filtro filtro)
        {
            return ServiceProv.ProductoAdm_GetListaResumen(filtro);
        }

    }

}