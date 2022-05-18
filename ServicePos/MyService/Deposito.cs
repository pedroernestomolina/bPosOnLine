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

        public DtoLib.ResultadoLista<DtoLibPos.Deposito.Lista.Ficha> Deposito_GetLista(DtoLibPos.Deposito.Lista.Filtro filtro)
        {
            return ServiceProv.Deposito_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFichaById(string id)
        {
            return ServiceProv.Deposito_GetFichaById(id);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFicha_ByCodigo(string codigo)
        {
            return ServiceProv.Deposito_GetFicha_ByCodigo(codigo);
        }

    }

}