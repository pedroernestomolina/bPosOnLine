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

        public DtoLib.ResultadoLista<DtoLibPos.Vendedor.Lista.Ficha> Vendedor_GetLista(DtoLibPos.Vendedor.Lista.Filtro filtro)
        {
            return ServiceProv.Vendedor_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Vendedor.Entidad.Ficha> Vendedor_GetFichaById(string id)
        {
            return ServiceProv.Vendedor_GetFichaById(id);
        }

    }

}