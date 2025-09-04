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
        public DtoLib.ResultadoLista<DtoLibPos.Moneda.Entidad.Ficha>
            Moneda_GetLista(DtoLibPos.Moneda.Filtro filtro)
        {
            return ServiceProv.Moneda_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Moneda.Entidad.Ficha>
            Moneda_GetFichaById(int id)
        {
            return ServiceProv.Moneda_GetFichaById(id);
        }
    }
}
